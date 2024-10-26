using CashFlow.Application.UseCases.User.Register;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCase.Test.Users.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    private RegisterUserUseCase CreateUseCase()
    {
        return new RegisterUserUseCase();
    }
    
}