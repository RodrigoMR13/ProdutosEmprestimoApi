using Application.Responses;
using MediatR;

namespace Application.Queries
{
    public class GetProdutoEmprestimoByIdQuery : IRequest<ProdutoEmprestimoResponse>
    {
        public long Id { get; set; }

        public GetProdutoEmprestimoByIdQuery() { }

        public GetProdutoEmprestimoByIdQuery(long id)
        {
            Id = id;
        }

    }
}
