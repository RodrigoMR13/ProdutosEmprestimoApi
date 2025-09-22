using Application.Dtos;

namespace UnitTests.Application.Dtos
{
    public class DetalhesParcelaTests
    {
        [Fact]
        public void ConstrutorDeveAtribuirValoresCorretamente()
        {
            // Arrange
            int numParcela = 2;
            decimal saldoDevedorInicial = 700.00m;
            decimal juros = 0.010m;
            decimal amortizacao = 200.00m;
            decimal saldoDevedorFinal = 500.00m;
            // Act
            DetalhesParcela detalhesParcela = new(numParcela, saldoDevedorInicial, juros, amortizacao, saldoDevedorFinal);
            // Assert
            Assert.Equal(numParcela, detalhesParcela.NumeroParcela);
            Assert.Equal(saldoDevedorInicial, detalhesParcela.SaldoDevedorInicial);
            Assert.Equal(juros, detalhesParcela.Juros);
            Assert.Equal(amortizacao, detalhesParcela.Amortizacao);
            Assert.Equal(saldoDevedorFinal, detalhesParcela.SaldoDevedorFinal);
            Assert.IsType<DetalhesParcela>(detalhesParcela);
            Assert.IsType<int>(detalhesParcela.NumeroParcela);
            Assert.IsType<decimal>(detalhesParcela.SaldoDevedorInicial);
            Assert.IsType<decimal>(detalhesParcela.Juros);
            Assert.IsType<decimal>(detalhesParcela.Amortizacao);
            Assert.IsType<decimal>(detalhesParcela.SaldoDevedorFinal);
        }
    }
}
