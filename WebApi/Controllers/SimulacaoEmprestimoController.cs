using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/emprestimos/simulacoes")]
    public class SimulacaoEmprestimoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SimulacaoEmprestimoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SimularEmprestimo([FromBody] SimularEmprestimoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
