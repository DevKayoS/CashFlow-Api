using CashFlow.Application.UseCases.User;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Users.Register;

public class PasswordValidatorTest
{
    [Fact]
    public void Sucess()
    {
        // arrange
        var validator = new RegisterUserValidator();
        var request = RequestsRegisterUserJsonBuilder.Build();
        
        // act
        var result = validator.Validate(request);
        
        // assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("          ")]
    [InlineData(null)]
    public void Error_Name_Empty(string name)
    {
        // arrange
        var validator = new RegisterUserValidator();
        var request = RequestsRegisterUserJsonBuilder.Build();
        request.Name = name;
        
        // act
        var result = validator.Validate(request);
        
        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("          ")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        // arrange
        var validator = new RegisterUserValidator();
        var request = RequestsRegisterUserJsonBuilder.Build();
        request.Email = email;
        
        // act
        var result = validator.Validate(request);
        
        // assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY));
    }
}