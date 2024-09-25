namespace CashFlow.Application.UseCases.Expenses.Register;

public interface IDeleteExpenseUseCase
{
    Task Execute(long id);
}