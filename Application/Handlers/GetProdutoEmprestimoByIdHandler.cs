using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Queries;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Handlers
{
    public class GetProdutoEmprestimoByIdHandler :
        IRequestHandler<GetProdutoEmprestimoByIdQuery, ProdutoEmprestimoResponse>
    {
        private readonly IProdutoEmprestimoRepository _produtoEmprestimoRepository;

        public GetProdutoEmprestimoByIdHandler(IProdutoEmprestimoRepository produtoEmprestimoRepository)
        {
            _produtoEmprestimoRepository = produtoEmprestimoRepository;
        }

        public Task<ProdutoEmprestimoResponse> Handle(
            GetProdutoEmprestimoByIdQuery query,
            CancellationToken cancellationToken)
        {
            ProdutoEmprestimo produtoEmprestimo = _produtoEmprestimoRepository.GetById(query.Id)
                ?? throw new ProdutoEmprestimoNotFoundException(query.Id);

            ProdutoEmprestimoResponse response = new()
            {
                Id = produtoEmprestimo.Id,
                Nome = produtoEmprestimo.Nome,
                TaxaJurosAnual = produtoEmprestimo.TaxaJurosAnual,
                PrazoMaximoMeses = produtoEmprestimo.PrazoMaximoMeses
            };
            
            return Task.FromResult(response);
        }
    }
}
