using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Report.GetExcel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _repository;

    public GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        
        if(expenses.Count == 0)
        {
            return [];
        }
        
        using var workbook = new XLWorkbook();

        workbook.Author = "Kayo Vinicius";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Arial";
        
       var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        
       InsertHeader(worksheet);

       var raw = 2;
       
       foreach (var expense in expenses)
       {
           worksheet.Cell($"A{raw}").Value = expense.Title;
           worksheet.Cell($"B{raw}").Value = expense.Date;
           worksheet.Cell($"C{raw}").Value = ConvertPayment.Convert(expense.PaymentType);
           
           worksheet.Cell($"D{raw}").Value = expense.Amount;
           worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"- {CURRENCY_SYMBOL} #,##0.00"; // ex: - R$12.00
               
           worksheet.Cell($"E{raw}").Value = expense.Description;

           raw++;
       }
        
       // ajustando o preenchimento das colunas automaticamente
       worksheet.Columns().AdjustToContents();

       var file = new MemoryStream();
       
       workbook.SaveAs(file);
       
        return file.ToArray();
    }

    
    private void InsertHeader(IXLWorksheet worksheet)
    {
        // colocando nome das colunas
        worksheet.Cell("A1").Value = ResourceReportsGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportsGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportsGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportsGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportsGenerationMessages.DESCRIPTION;
        //colocando font bold no no header 
        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        //colocando background 
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#f5c2b6");
        // colocando as colunas centralizadas        
        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}