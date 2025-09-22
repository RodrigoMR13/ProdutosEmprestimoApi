using Application.Commands;
using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Handlers;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Handlers
{
    public class UpdateProdutoEmprestimoHandlerTests
    {
        private readonly Mock<IProdutoEmprestimoRepository> _repositoryMock;
        private readonly UpdateProdutoEmprestimoHandler _handler;

        public UpdateProdutoEmprestimoHandlerTests()
        {
            _repositoryMock = new Mock<IProdutoEmprestimoRepository>();
            _handler = new UpdateProdutoEmprestimoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleDeveChamarRepositoryUpdateUmaVez()
        {
            // Arrange
            var produtoEmprestimo = new ProdutoEmprestimo(1L, "Produto Teste", 0.18m, 24);
            var command = new UpdateProdutoEmprestimoCommand(1L, "Novo Nome Produto", 0.18m, 24);

            _repositoryMock
                .Setup(r => r.GetById(command.Id))
                .Returns(produtoEmprestimo);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Update(It.Is<ProdutoEmprestimo>(p =>
                p.Id == command.Id &&
                p.Nome == command.Nome &&
                p.TaxaJurosAnual == command.TaxaJurosAnual &&
                p.PrazoMaximoMeses == command.PrazoMaximoMeses
            )), Times.Once);
        }


        [Fact]
        public async Task HandleDeveRetornarResponseTrue()
        {
            var produtoEmprestimo = new ProdutoEmprestimo(1L, "Produto Teste", 0.18m, 24);
            var command = new UpdateProdutoEmprestimoCommand(1L, "Novo Nome Produto", 0.18m, 24);
            var produtoEmprestimoAtualizado = new ProdutoEmprestimo(produtoEmprestimo.Id, command.Nome,
                command.TaxaJurosAnual, command.PrazoMaximoMeses);

            _repositoryMock
                .Setup(r => r.GetById(command.Id))
                .Returns(produtoEmprestimo);

            _repositoryMock
                .Setup(r => r.Update(produtoEmprestimoAtualizado));

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task QuandoGetByIdRetornarNuloDeveJogarExceptionComMensagemCorreta()
        {
            // Arrange
            var command = new UpdateProdutoEmprestimoCommand(1L, "Novo Nome Produto", 0.18m, 24);

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
