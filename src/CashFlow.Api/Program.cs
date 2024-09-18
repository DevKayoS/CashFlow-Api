using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// responsavel por fazer a api entender o filtro
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// fazendo com o que a API entenda que tem um middleware
app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();