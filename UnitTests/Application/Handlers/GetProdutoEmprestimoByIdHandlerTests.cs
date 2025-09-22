using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Handlers;
using Application.Queries;
using Application.Responses;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Handlers
{
    public class GetProdutoEmprestimoByIdHandlerTests
    {
        private readonly Mock<IProdutoEmprestimoRepository> _repositoryMock;
        private readonly GetProdutoEmprestimoByIdHandler _handler;

        public GetProdutoEmprestimoByIdHandlerTests()
        {
            _repositoryMock = new Mock<IProdutoEmprestimoRepository>();
            _handler = new GetProdutoEmprestimoByIdHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleDeveChamarRepositoryGetByIdUmaVez()
        {
            // Arrange
            var query = new GetProdutoEmprestimoByIdQuery(1L);
            var produto = new ProdutoEmprestimo(1L, "Produto Teste", 0.15m, 12);
            _repositoryMock
                .Setup(r => r.GetById(query.Id))
                .Returns(produto);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.GetById(query.Id), Times.Once);
        }

        [Fact]
        public async Task HandleDeveRetornarProdutoEmprestimoResponseValido()
        {
            // Arrange
            var query = new GetProdutoEmprestimoByIdQuery(1L);
            var produto = new ProdutoEmprestimo(1L, "Produto Teste", 0.15m, 12);
            _repositoryMock
                .Setup(r => r.GetById(query.Id))
                .Returns(produto);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(query.Id, response.Id);
            Assert.IsType<ProdutoEmprestimoResponse>(response);
        }

        [Fact]
        public async Task QuandoGetByIdRetornarNuloDeveJogarExceptionComMensagemCorreta()
        {
            // Arrange
            var query = new GetProdutoEmprestimoByIdQuery(1L);
            _repositoryMock
                .Setup(r => r.GetById(query.Id))
                .Returns((ProdutoEmprestimo?)null);

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<ProdutoEmprestimoNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
            Assert.Equal($"Produto Empréstimo com Id {query.Id} não foi encontrado.", ex.Message);
        }
    }
}
