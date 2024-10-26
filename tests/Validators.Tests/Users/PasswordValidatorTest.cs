using CashFlow.Application.UseCases.User;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.Users.Register;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("          ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("Aaaaaaaa")]
    [InlineData("AAAAAAAAA")]
    [InlineData("Aaaaaaaa1")]
    public void Error_Password_Invalid(string password)
    {
        // arrange
        var validator = new PasswordValidator<RequestRegisterUserJson>();
        
        // act
        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);
        
        // assert
        result.Should().BeFalse();
    }
}