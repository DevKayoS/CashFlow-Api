using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public interface IGetAllExpensesUseCase
{
    Task<ResponseExpensesJson> Execute();
}