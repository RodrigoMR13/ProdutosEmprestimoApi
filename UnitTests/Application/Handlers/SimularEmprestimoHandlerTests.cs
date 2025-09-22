using Application.Commands;
using Application.Common.Interfaces;
using Application.Dtos;
using Application.Exceptions;
using Application.Handlers;
using Application.UseCases;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Handlers
{
    public class SimularEmprestimoHandlerTests
    {
        private readonly Mock<IProdutoEmprestimoRepository> _repositoryMock;
        private readonly Mock<IPriceCalculatorService> _priceCalculatorMock;
        private readonly Mock<ICacheService> _cache;
        private readonly SimularEmprestimoHandler _handler;

        public SimularEmprestimoHandlerTests()
        {
            _repositoryMock = new Mock<IProdutoEmprestimoRepository>();
            _priceCalculatorMock = new Mock<IPriceCalculatorService>();
            _cache = new Mock<ICacheService>();
            _handler = new SimularEmprestimoHandler(_repositoryMock.Object, _priceCalculatorMock.Object, _cache.Object);
        }

        [Fact]
        public async Task HandleDeveRetornarSimulacaoCorretaQuandoProdutoExiste()
        {
            // Arrange
            var produto = new ProdutoEmprestimo(1L, "Produto Teste", 0.18m, 24);
            var command = new SimularEmprestimoCommand(produto.Id, 1000.00m, 3);

            _repositoryMock.Setup(r => r.GetById(produto.Id)).Returns(produto);

            var parcelas = new List<DetalhesParcela>
            {
                new(1, 1000m, 13.89m, 328.75m, 671.25m),
                new(2, 671.25m, 9.32m, 333.31m, 337.94m),
                new(3, 337.94m, 4.69m, 337.94m, 0m)
            };

            _priceCalculatorMock
                .Setup(p => p.CalcularParcelas(command.ValorSolicitado, command.PrazoMeses, It.IsAny<decimal>()))
                .Returns(parcelas);

            _priceCalculatorMock
                .Setup(p => p.GetParcelaMensal(command.ValorSolicitado, command.PrazoMeses, It.IsAny<decimal>()))
                .Returns(342.63m);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);

            Assert.Equal(produto.Id, response.Produto.Id);
            Assert.Equal(produto.Nome, response.Produto.Nome);
            Assert.Equal(produto.TaxaJurosAnual, response.Produto.TaxaJurosAnual);
            Assert.Equal(produto.PrazoMaximoMeses, response.Produto.PrazoMaximoMeses);

            Assert.Equal(command.ValorSolicitado, response.ValorSolicitado);
            Assert.Equal(command.PrazoMeses, response.PrazoMeses);
            Assert.Equal(1027.90m, response.ValorTotalComJuros);
            Assert.Equal(342.63m, response.ParcelaMensal);
            Assert.Equal(parcelas.Count, response.DetalhesParcelas.Count());

            _priceCalculatorMock.Verify(p => p.CalcularParcelas(command.ValorSolicitado, command.PrazoMeses, It.IsAny<decimal>()), Times.Once);
            _priceCalculatorMock.Verify(p => p.GetParcelaMensal(command.ValorSolicitado, command.PrazoMeses, It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public async Task HandleDeveLancarExcecaoQuandoProdutoNaoExiste()
        {
            // Arrange
            var command = new SimularEmprestimoCommand(1L, 1000.00m, 3);

            _repositoryMock.Setup(r => r.GetById(command.IdProduto)).Returns((ProdutoEmprestimo?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ProdutoEmprestimoNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Produto Empréstimo com Id {command.IdProduto} não foi encontrado.", ex.Message);
        }
    }
}
