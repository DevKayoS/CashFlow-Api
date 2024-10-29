﻿using CashFlow.Application.UseCases.User.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
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
    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        
        var useCase = CreateUseCase();
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
    }

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = PasswordEncripeterBuilder.Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder().Build();
        
        return new RegisterUserUseCase(
            mapper, 
            passwordEncripter,
            readOnlyRepository,
            writeOnlyRepository,
            unitOfWork,
            tokenGenerator
            );
    }
}