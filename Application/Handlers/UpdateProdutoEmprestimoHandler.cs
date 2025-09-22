using Application.Commands;
using Application.Common.Interfaces;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Handlers
{
    public class UpdateProdutoEmprestimoHandler :
        IRequestHandler<UpdateProdutoEmprestimoCommand, bool>
    {
        private readonly IProdutoEmprestimoRepository _produtoEmprestimoRepository;

        public UpdateProdutoEmprestimoHandler(IProdutoEmprestimoRepository produtoEmprestimoRepository)
        {
            _produtoEmprestimoRepository = produtoEmprestimoRepository;
        }

        public Task<bool> Handle(UpdateProdutoEmprestimoCommand command, CancellationToken cancellationToken)
        {
            ProdutoEmprestimo produtoEmprestimo = _produtoEmprestimoRepository.GetById(command.Id)
                ?? throw new ProdutoEmprestimoNotFoundException(command.Id);

            var produtoEmprestimoAtualizado = new ProdutoEmprestimo(produtoEmprestimo.Id, command.Nome,
                command.TaxaJurosAnual, command.PrazoMaximoMeses);

            _produtoEmprestimoRepository.Update(produtoEmprestimoAtualizado);

            return Task.FromResult(true);
        }
    }
}
