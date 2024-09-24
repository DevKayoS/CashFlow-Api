using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RequestRegisterExpenseJson request, [FromServices] IRegisterExpenseUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
