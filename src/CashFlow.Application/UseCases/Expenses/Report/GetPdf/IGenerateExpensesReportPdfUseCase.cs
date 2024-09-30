namespace CashFlow.Application.UseCases.Expenses.Register.Report.GetPdf;

public interface IGenerateExpensesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}