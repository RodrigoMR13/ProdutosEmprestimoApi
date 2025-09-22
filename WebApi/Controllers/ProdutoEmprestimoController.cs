using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/emprestimos/produtos")]
    public class ProdutoEmprestimoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutoEmprestimoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() 
        {
            var request = new GetProdutosEmprestimoQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id:long}", Name = "GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var request = new GetProdutoEmprestimoByIdQuery(id);
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProdutoEmprestimoCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProdutoEmprestimoCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteByIdAsync(long id) 
        {
            var request = new DeleteProdutoEmprestimoCommand(id);
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
