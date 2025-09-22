using Application.Responses;
using MediatR;

namespace Application.Commands
{
    public class CreateProdutoEmprestimoCommand : IRequest<ProdutoEmprestimoResponse>
    {
        public string Nome { get; set; }
        public decimal TaxaJurosAnual { get; set; }
        public short PrazoMaximoMeses { get; set; }

        public CreateProdutoEmprestimoCommand() { }

        public CreateProdutoEmprestimoCommand(string nome, decimal txJurosAnual, short prazoMaxMeses)
        {
            Nome = nome;
            TaxaJurosAnual = txJurosAnual;
            PrazoMaximoMeses = prazoMaxMeses;
        }
    }
}
