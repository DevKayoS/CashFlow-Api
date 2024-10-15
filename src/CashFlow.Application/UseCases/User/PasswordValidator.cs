﻿using System.ComponentModel.DataAnnotations;
using CashFlow.Communication.Requests;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.User;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        
    }
}