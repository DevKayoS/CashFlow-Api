using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.User.Login;

public interface ILoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginUserJson request);
}