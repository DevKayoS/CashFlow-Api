using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enum;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    // com o [Fact] faz com que essa funcao seja um teste de unidade
    [Fact]
    public void Success()
    {
        //Arrange (pegando os dados)
        var validator = new RegisterExpenseValidator();
        var request = new RequestRegisterExpenseJson
        {
            Amount = 100,
            Date = DateTime.Now.AddDays(-1),
            Description = "Description",
            Title = "Apple",
            PaymentType = PaymentType.DebitCard
        };
        //Act (criando acao para testar os dados criados)
        var result = validator.Validate(request);

        //Assert (verficar se a resposta seria oq era esperado)
        // espero que seja verdadeiro a request
        Assert.True(result.IsValid == true);
    }
}