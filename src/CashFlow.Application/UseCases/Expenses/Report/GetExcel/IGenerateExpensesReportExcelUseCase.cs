namespace CashFlow.Application.UseCases.Report.GetExcel;

public interface IGenerateExpensesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}