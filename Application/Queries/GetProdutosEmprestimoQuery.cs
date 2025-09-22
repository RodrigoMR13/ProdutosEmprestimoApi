using Application.Responses;
using MediatR;

namespace Application.Queries
{
    public class GetProdutosEmprestimoQuery : IRequest<IEnumerable<ProdutoEmprestimoResponse>>
    {
    }
}
