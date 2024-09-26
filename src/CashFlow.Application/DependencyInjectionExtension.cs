using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Report.GetExcel;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCase(services);
        AddAutoMapper(services);
    }

    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();        
        services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();        
        services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();        
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();        
        services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();        
        services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();        
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
}