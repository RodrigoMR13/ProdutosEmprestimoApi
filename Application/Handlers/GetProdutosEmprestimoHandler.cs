using Application.Queries;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Handlers
{
    public class GetProdutosEmprestimoHandler : 
        IRequestHandler<GetProdutosEmprestimoQuery, IEnumerable<ProdutoEmprestimoResponse>>
    {
        private readonly IProdutoEmprestimoRepository _produtoEmprestimoRepository;

        public GetProdutosEmprestimoHandler(IProdutoEmprestimoRepository produtoEmprestimoRepository)
        {
            _produtoEmprestimoRepository = produtoEmprestimoRepository;
        }

        public Task<IEnumerable<ProdutoEmprestimoResponse>> Handle(
            GetProdutosEmprestimoQuery query,
            CancellationToken cancellationToken)
        {
            IEnumerable<ProdutoEmprestimo> produtos = _produtoEmprestimoRepository.GetAll();

            IEnumerable<ProdutoEmprestimoResponse> response = produtos.Select(p => new ProdutoEmprestimoResponse
            {
                Id = p.Id,
                Nome = p.Nome,
                TaxaJurosAnual = p.TaxaJurosAnual,
                PrazoMaximoMeses = p.PrazoMaximoMeses
            });

            return Task.FromResult(response.AsEnumerable());
        }
    }
}
