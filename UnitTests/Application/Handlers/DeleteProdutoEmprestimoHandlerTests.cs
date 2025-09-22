using Application.Commands;
using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Handlers;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Handlers
{
    public class DeleteProdutoEmprestimoHandlerTests
    {
        private readonly Mock<IProdutoEmprestimoRepository> _repositoryMock;
        private readonly DeleteProdutoEmprestimoHandler _handler;

        public DeleteProdutoEmprestimoHandlerTests() 
        {
            _repositoryMock = new Mock<IProdutoEmprestimoRepository>();
            _handler = new DeleteProdutoEmprestimoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleDeveChamarRepositoryDeleteUmaVez()
        {
            // Arrange
            var command = new DeleteProdutoEmprestimoCommand(1L);
            var produto = new ProdutoEmprestimo(1L, "Produto Teste", 0.15m, 12);
            _repositoryMock
                .Setup(r => r.GetById(command.Id))
                .Returns(produto);
            _repositoryMock
                .Setup(r => r.Delete(command.Id));

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Delete(command.Id), Times.Once);
        }

        [Fact]
        public async Task HandleDeveRetornarResponseTrue()
        {
            // Arrange
            var command = new DeleteProdutoEmprestimoCommand(1L);
            var produto = new ProdutoEmprestimo(1L, "Produto Teste", 0.15m, 12);
            _repositoryMock
                .Setup(r => r.GetById(command.Id))
                .Returns(produto);
            _repositoryMock
                .Setup(r => r.Delete(command.Id));

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task QuandoGetByIdRetornarNuloDeveJogarExceptionComMensagemCorreta()
        {
            // Arrange
            var command = new DeleteProdutoEmprestimoCommand(1L);
            _repositoryMock
                .Setup(r => r.GetById(command.Id))
                .Returns((ProdutoEmprestimo?)null);

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<ProdutoEmprestimoNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Produto Empréstimo com Id {command.Id} não foi encontrado.", ex.Message);
        }
    }
}
