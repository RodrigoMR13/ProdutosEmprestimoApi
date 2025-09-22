using Application.Responses;
using MediatR;

namespace Application.Commands
{
    public class SimularEmprestimoCommand : IRequest<SimulacaoEmprestimoResponse>
    {
        public long IdProduto { get; set; }
        public decimal ValorSolicitado { get; set; }
        public short PrazoMeses { get; set; }

        public SimularEmprestimoCommand() { }

        public SimularEmprestimoCommand(long idProduto, decimal valorSolicitado, short prazoMeses)
        {
            IdProduto = idProduto;
            ValorSolicitado = valorSolicitado;
            PrazoMeses = prazoMeses;
        }
    }
}
