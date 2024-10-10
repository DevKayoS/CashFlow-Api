using System.Reflection;
using CashFlow.Application.UseCases.Expenses.Register.Report.GetPdf.Colors;
using CashFlow.Application.UseCases.Expenses.Register.Report.GetPdf.Fonts;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Register.Report.GetPdf;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _repository;
    
    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }
    
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);

        if (expenses.Count <= 0)
        {
            return [];
        }
        // generate void document
        var document = CreateDocument(month);
        var page = CreatePage(document);
        
        CreateHeaderWithProfilePhotoAndName(page);
        
        var totalExpenses = expenses.Sum(expense => expense.Amount);
        CreateTotalSpentSection(page, month, totalExpenses);
        // added table for amounts
        foreach (var expense in expenses)
        {
            var table = CreateExpenseTable(page);
            var row = table.AddRow();
            row.Height = 25;
            
            row.Cells[0].AddParagraph(expense.Title);
            row.Cells[0].Format.Font = new Font
            {
                Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK
            };
            row.Cells[0].Shading.Color = ColorsHelper.RED_LIGHT;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            // merge 2 columns for to right
            row.Cells[0].MergeRight = 2;
            row.Cells[0].Format.LeftIndent = 20;
            
            row.Cells[3].AddParagraph(ResourceReportsGenerationMessages.AMOUNT);
            row.Cells[3].Format.Font = new Font
            {
                Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE
            };
            row.Cells[3].Shading.Color = ColorsHelper.RED_DARK;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row = table.AddRow();
            row.Height = 25;
            row.Cells[0].AddParagraph(expense.Date.ToString("D"));
            row.Cells[0].Format.Font = new Font
                { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            row.Cells[0].Shading.Color = ColorsHelper.GREEN_DARK;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            
            row.Cells[0].Format.LeftIndent = 20;
            
            row.Cells[1].AddParagraph(expense.Date.ToString("t"));
            row.Cells[1].Format.Font = new Font
                { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            row.Cells[1].Shading.Color = ColorsHelper.GREEN_DARK;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            

            row.Cells[2].AddParagraph(ConvertPayment.Convert(expense.PaymentType));
            row.Cells[2].Format.Font = new Font
                { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            row.Cells[2].Shading.Color = ColorsHelper.GREEN_DARK;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            
            row.Cells[3].AddParagraph($"-{CURRENCY_SYMBOL} {expense.Amount.ToString()}");
            row.Cells[3].Format.Font = new Font
                { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
            row.Cells[3].Shading.Color = ColorsHelper.WHITE;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;        
            
            table.AddRow().Height = 30;            
        }
        
        
        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"Expenses for {month.ToString("Y")}";
        document.Info.Author = "Kayo Vinicius";
        
        //font default is Raleway regular
        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        
        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        // setting document margin
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        
        return section;
    }
    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };
        
        renderer.RenderDocument();
        
        // saving in memory
        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        //table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();
        /*
         code to add image to the header pdf file
        var assembly = Assembly.GetExecutingAssembly();
        var directoryName  = Path.GetDirectoryName(assembly.Location);
        row.Cells[0].AddImage(Path.Combine(directoryName!, "Logo", "ProfilePhoto.png"));
        */
        row.Cells[0].AddParagraph("Hey, Kayo Vinicius");
        row.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        
        return table;
    }
    private void CreateTotalSpentSection(Section page, DateOnly month, decimal totalExpenses)
    {
        var title = string.Format("Total spent in {0}", month.ToString("Y"));

        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";
        
        paragraph.AddFormattedText(title, new Font {Name = FontHelper.RALEWAY_REGULAR, Size = 15});
        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalExpenses}", new Font { Name = FontHelper.WORKSANS_BLACK , Size = 50});
    }
}