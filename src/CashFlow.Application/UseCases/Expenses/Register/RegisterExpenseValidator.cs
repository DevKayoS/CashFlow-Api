using CashFlow.Communication.Enum;
using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

// usando fluent validator
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        // fazendo encadeamento de chamadas
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("The title is required");
        RuleFor(expense => expense.Amount).GreaterThanOrEqualTo(0).WithMessage("The amount must be greater than 0");
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Expanses cannot be for the future");
        RuleFor(expanse => expanse.PaymentType).IsInEnum().WithMessage("Payment Type is invalid");
    }
    
}