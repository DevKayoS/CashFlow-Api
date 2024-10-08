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

        var table = page.AddTable();
        table.AddColumn("300");
        
        var row = table.AddRow();
        //row.Cells[0].AddImage("caminho do arquivo com a imagem");
        row.Cells[0].AddParagraph("Hey, Kayo Vinicius");
        row.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";
        
        var title = string.Format("Total spent in {0}", month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font {Name = FontHelper.RALEWAY_REGULAR, Size = 15});
        paragraph.AddLineBreak();

        var totalExpenses = expenses.Sum(expense => expense.Amount);
        
        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalExpenses}", new Font { Name = FontHelper.WORKSANS_BLACK , Size = 50});
        
        
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
/// <summary>
///  this function should be able to return the pdf archive
/// </summary>
/// <param name="document"></param>
/// <returns></returns>
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
}