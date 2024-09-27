using CashFlow.Domain.Reports;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Report.GetExcel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly month)
    {
        var workbook = new XLWorkbook();

        workbook.Author = "Kayo Vinicius";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";
        
       var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        
       InsertHeader(worksheet);

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