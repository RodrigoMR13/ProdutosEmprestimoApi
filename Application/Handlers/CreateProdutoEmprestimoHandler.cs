using Application.Commands;
using Application.Common.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Handlers
{
    public class CreateProdutoEmprestimoHandler :
        IRequestHandler<CreateProdutoEmprestimoCommand, ProdutoEmprestimoResponse>
    {
        private readonly IProdutoEmprestimoRepository _produtoEmprestimoRepository;

        public CreateProdutoEmprestimoHandler(IProdutoEmprestimoRepository produtoEmprestimoRepository)
        {
            _produtoEmprestimoRepository = produtoEmprestimoRepository;
        }

        public Task<ProdutoEmprestimoResponse> Handle(CreateProdutoEmprestimoCommand command, CancellationToken cancellationToken)
        {
            long id = _produtoEmprestimoRepository.Add(command.Nome, command.TaxaJurosAnual, command.PrazoMaximoMeses);
            ProdutoEmprestimoResponse response = new()
            {
                Id = id,
                Nome = command.Nome,
                TaxaJurosAnual = command.TaxaJurosAnual,
                PrazoMaximoMeses = command.PrazoMaximoMeses
            };
            return Task.FromResult(response);
        }
    }
}
