using Application.Commands;
using Application.Common.Interfaces;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers
{
    public class DeleteProdutoEmprestimoHandler : 
        IRequestHandler<DeleteProdutoEmprestimoCommand, bool>
    {
        private readonly IProdutoEmprestimoRepository _produtoEmprestimoRepository;

        public DeleteProdutoEmprestimoHandler(IProdutoEmprestimoRepository produtoEmprestimoRepository)
        {
            _produtoEmprestimoRepository = produtoEmprestimoRepository;
        }

        public Task<bool> Handle(DeleteProdutoEmprestimoCommand command, CancellationToken cancellationToken)
        {
            _ = _produtoEmprestimoRepository.GetById(command.Id)
                ?? throw new ProdutoEmprestimoNotFoundException(command.Id);

            _produtoEmprestimoRepository.Delete(command.Id);

            return Task.FromResult(true);
        }
    }
}
