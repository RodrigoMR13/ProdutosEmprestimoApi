using MediatR;

namespace Application.Commands
{
    public class DeleteProdutoEmprestimoCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public DeleteProdutoEmprestimoCommand() { }

        public DeleteProdutoEmprestimoCommand(long id)
        {
            Id = id;
        }

    }
}
