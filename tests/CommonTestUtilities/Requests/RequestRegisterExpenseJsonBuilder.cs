﻿using Bogus;
using CashFlow.Communication.Enum;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
    public static RequestExpenseJson Build()
    {
        /*
         Pode ser feito da seguinte forma tbm 
        var faker = new Faker();
        var request = new RequestRegisterExpenseJson
        {
            Title = faker.Commerce.Product(),
            Date = faker.Date.Past(),
            Description = faker.Lorem.Sentence(20),
            PaymentType = faker.PickRandom<PaymentType>(),
        };
        */

       return new Faker<RequestExpenseJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker  => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
    }
}