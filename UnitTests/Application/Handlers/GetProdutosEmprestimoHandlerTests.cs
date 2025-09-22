using Application.Common.Interfaces;
using Application.Handlers;
using Application.Queries;
using Application.Responses;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Handlers
{
    public class GetProdutosEmprestimoHandlerTests
    {
        private readonly Mock<IProdutoEmprestimoRepository> _repositoryMock;
        private readonly GetProdutosEmprestimoHandler _handler;

        public GetProdutosEmprestimoHandlerTests()
        {
            _repositoryMock = new Mock<IProdutoEmprestimoRepository>();
            _handler = new GetProdutosEmprestimoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleDeveChamarRepositoryGetAllUmaVez()
        {
            // Arrange
            var query = new GetProdutosEmprestimoQuery();
            var listProdutos = new List<ProdutoEmprestimo>()
            {
                new(1L, "Produto Teste 1", 0.15m, 12),
                new(2L, "Produto Teste 2", 0.18m, 24)
            };
            _repositoryMock
                .Setup(r => r.GetAll())
                .Returns(listProdutos);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public async Task HandleDeveRetornarListaDeProdutoEmprestimoResponseValidos()
        {
            // Arrange
            var query = new GetProdutosEmprestimoQuery();
            var listProdutos = new List<ProdutoEmprestimo>()
            {
                new(1L, "Produto Teste 1", 0.15m, 12),
                new(2L, "Produto Teste 2", 0.18m, 24)
            };
            _repositoryMock
                .Setup(r => r.GetAll())
                .Returns(listProdutos);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<ProdutoEmprestimoResponse>>(response);
            Assert.Collection(response,
                item =>
                {
                    Assert.Equal(1L, item.Id);
                    Assert.Equal("Produto Teste 1", item.Nome);
                    Assert.Equal(0.15m, item.TaxaJurosAnual);
                    Assert.Equal(12, item.PrazoMaximoMeses);
                },
                item =>
                {
                    Assert.Equal(2L, item.Id);
                    Assert.Equal("Produto Teste 2", item.Nome);
                    Assert.Equal(0.18m, item.TaxaJurosAnual);
                    Assert.Equal(24, item.PrazoMaximoMeses);
                }
            );
        }

        [Fact]
        public async Task HandleDeveRetornarListaVaziaQuandoNaoExistemProdutos()
        {
            // Arrange
            var query = new GetProdutosEmprestimoQuery();
            _repositoryMock
                .Setup(r => r.GetAll())
                .Returns([]);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Empty(response);
        }
    }
}
