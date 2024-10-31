﻿using CashFlow.Application.UseCases.User.Login;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCase.Test.Users.Login;

public class LoginUserUseCaseTest
{
   [Fact]
   public async Task Success()
   {
      var user = UserBuilder.Build();
    
      var request = RequestLoginUserJsonBuilder.Build();
      request.Email = user.Email;
      
      var useCase = CreateUseCase(user, request.Password);

      var result = await useCase.Execute(request);
      
      result.Should().NotBeNull();
      result.Name.Should().Be(user.Name);
      result.Token.Should().NotBeNullOrWhiteSpace();
   }

   [Fact]
   public async Task Error_User_Not_Found()
   {
      var user = UserBuilder.Build();

      var request = RequestLoginUserJsonBuilder.Build();

      var useCase = CreateUseCase(user, request.Password);

      var act = async () => await useCase.Execute(request);

      var result = await act.Should().ThrowAsync<InvalidLoginException>();
      result.Where(ex =>
         ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));

   }

   [Fact]
   public async Task Error_Password_Not_Match()
   {
      var user = UserBuilder.Build();

      var request = RequestLoginUserJsonBuilder.Build();
      request.Email = user.Email;

      var useCase = CreateUseCase(user);

      var act = async () => await useCase.Execute(request);

      var result = await act.Should().ThrowAsync<InvalidLoginException>();
      result.Where(ex =>
         ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
  
   }

   private LoginUserUseCase CreateUseCase(User user, string? password = null)
   {
      var passwordEncripter = new PasswordEncrypterBuilder().Verify(password).Build();
      var tokenGenerator = JwtTokenGeneratorBuilder.Build();
      var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
      

      return new LoginUserUseCase(userReadOnlyRepository, passwordEncripter, tokenGenerator);
   }
}