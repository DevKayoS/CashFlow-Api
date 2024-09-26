using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Register;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}