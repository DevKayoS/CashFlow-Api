namespace CashFlow.Application.UseCases.Report.GetExcel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly month)
    {
        return new byte[1];
    }
}