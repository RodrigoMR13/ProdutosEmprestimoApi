using Application.UseCases;

namespace UnitTests.Application.UseCases
{
    public class PriceCalculatorServiceTests
    {
        private readonly IPriceCalculatorService _service;

        public PriceCalculatorServiceTests()
        {
            _service = new PriceCalculatorService();
        }

        [Fact]
        public void GetParcelaMensalDeveRetornarValorEsperado()
        {
            // Arrange
            decimal valor = 10000m;
            int periodo = 12;
            decimal taxaJurosMensal = 0.0138884303484099m;

            // Act
            var parcela = _service.GetParcelaMensal(valor, periodo, taxaJurosMensal);

            // Assert
            Assert.Equal(910.46m, Math.Round(parcela, 2));
        }

        [Fact]
        public void CalcularParcelasDeveGerarListaComQtdParcelasCorreta()
        {
            // Arrange
            decimal valor = 10000m;
            int periodo = 12;
            decimal taxaJurosMensal = 0.0138884303484099m;

            // Act
            var parcelas = _service.CalcularParcelas(valor, periodo, taxaJurosMensal);

            // Assert
            Assert.NotNull(parcelas);
            Assert.Equal(periodo, parcelas.Count);
        }

        [Fact]
        public void CalcularParcelasSaldoFinalDeveSerZero()
        {
            // Arrange
            decimal valor = 10000m;
            int periodo = 12;
            decimal taxaJuros = 0.01388m;

            // Act
            var parcelas = _service.CalcularParcelas(valor, periodo, taxaJuros);

            // Assert
            Assert.Equal(0, parcelas.Last().SaldoDevedorFinal);
        }

        [Fact]
        public void CalcularParcelasPrimeiraParcelaDeveConterJurosEAmortizacaoCorretos()
        {
            // Arrange
            decimal valor = 10000m;
            int periodo = 12;
            decimal taxaJuros = 0.01388m;

            // Act
            var parcelas = _service.CalcularParcelas(valor, periodo, taxaJuros);
            var primeira = parcelas.First();

            // Assert
            Assert.Equal(10000m, primeira.SaldoDevedorInicial);
            Assert.Equal(138.80m, primeira.Juros);
            Assert.Equal(771.62m, primeira.Amortizacao);
        }

        [Theory]
        [InlineData(10000, 0, 0.01)]
        [InlineData(10000, 12, 0.00)]
        public void GetParcelaMensalComPeriodoOuTaxaDeJurosZeroDeveLancarExcecao(decimal valor, int periodo, decimal taxa)
        {
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
                _service.GetParcelaMensal(valor, periodo, taxa));
        }

        [Theory]
        [InlineData(10000, 0, 0.01)]
        [InlineData(10000, 12, 0.00)]
        public void CalcularParcelasComPeriodoOuTaxaDeJurosZeroDeveLancarExcecao(decimal valor, int periodo, decimal taxa)
        {
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(() =>
                _service.CalcularParcelas(valor, periodo, taxa));
        }
    }
}
