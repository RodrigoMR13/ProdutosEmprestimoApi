using MediatR;

namespace Application.Commands
{
    public class UpdateProdutoEmprestimoCommand : IRequest<bool>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal TaxaJurosAnual { get; set; }
        public short PrazoMaximoMeses { get; set; }

        public UpdateProdutoEmprestimoCommand() { }

        public UpdateProdutoEmprestimoCommand(long id, string nome, decimal txJurosAnual, short prazoMaxMeses)
        {
            Id = id;
            Nome = nome;
            TaxaJurosAnual = txJurosAnual;
            PrazoMaximoMeses = prazoMaxMeses;
        }
    }
}
