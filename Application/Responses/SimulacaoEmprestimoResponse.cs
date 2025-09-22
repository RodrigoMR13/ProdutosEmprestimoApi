using Application.Dtos;
using System.Text.Json.Serialization;

namespace Application.Responses
{
    public class SimulacaoEmprestimoResponse
    {
        public ProdutoEmprestimoResponse Produto { get; set; }
        public decimal ValorSolicitado { get; set; }
        public short PrazoMeses { get; set; }
        public decimal TaxaJurosEfetivaMensal { get; set; }
        public decimal ValorTotalComJuros { get; set; }
        public decimal ParcelaMensal { get; set; }
        [JsonPropertyName("memoriaCalculo")]
        public IEnumerable<DetalhesParcela> DetalhesParcelas { get; set; }

        public SimulacaoEmprestimoResponse(
            ProdutoEmprestimoResponse produto,
            decimal valorSolicitado,
            short prazoMeses,
            decimal taxaJurosEfetivaMensal,
            decimal valorTotalComJuros,
            decimal parcelaMensal,
            IEnumerable<DetalhesParcela> detalhesParcelas)
        {
            Produto = produto;
            ValorSolicitado = valorSolicitado;
            PrazoMeses = prazoMeses;
            TaxaJurosEfetivaMensal = taxaJurosEfetivaMensal;
            ValorTotalComJuros = valorTotalComJuros;
            ParcelaMensal = parcelaMensal;
            DetalhesParcelas = detalhesParcelas;
        }
    }
}
