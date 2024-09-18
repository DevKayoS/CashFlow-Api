using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enum;
using CashFlow.Communication.Requests;
using CommonTestUtilities.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    // com o [Fact] faz com que essa funcao seja um teste de unidade
    [Fact]
    public void Success()
    {
    //Arrange (pegando os dados)
        var validator = new RegisterExpenseValidator();
        
        //pegando dados falsos
         var request = RequestRegisterExpenseJsonBuilder.Build();
      
        
        //Act (criando acao para testar os dados criados)
        var result = validator.Validate(request);

        //Assert (verficar se a resposta seria oq era esperado)
        // espero que seja verdadeiro a request
        Assert.True(result.IsValid == true);
    }
}