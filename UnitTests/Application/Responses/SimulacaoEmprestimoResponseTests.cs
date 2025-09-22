using Application.Dtos;
using Application.Responses;

namespace UnitTests.Application.Responses
{
    public class SimulacaoEmprestimoResponseTests
    {
        [Fact]
        public void ConstrutorDeveAtribuirValoresCorretamente()
        {
            // Arrange
            var produto = new ProdutoEmprestimoResponse();
            decimal valorSolicitado = 10000m;
            short prazoMeses = 24;
            decimal taxaJurosEfetivaMensal = 1.5m;
            decimal valorTotalComJuros = 12000m;
            decimal parcelaMensal = 500m;

            var detalhesParcelas = new List<DetalhesParcela>
            {
                new(),
                new()
            };

            // Act
            var response = new SimulacaoEmprestimoResponse(
                produto,
                valorSolicitado,
                prazoMeses,
                taxaJurosEfetivaMensal,
                valorTotalComJuros,
                parcelaMensal,
                detalhesParcelas
            );

            // Assert
            Assert.Equal(produto, response.Produto);
            Assert.Equal(valorSolicitado, response.ValorSolicitado);
            Assert.Equal(prazoMeses, response.PrazoMeses);
            Assert.Equal(taxaJurosEfetivaMensal, response.TaxaJurosEfetivaMensal);
            Assert.Equal(valorTotalComJuros, response.ValorTotalComJuros);
            Assert.Equal(parcelaMensal, response.ParcelaMensal);
            Assert.Equal(detalhesParcelas, response.DetalhesParcelas);
            Assert.IsType<SimulacaoEmprestimoResponse>(response);
        }
    }
}
