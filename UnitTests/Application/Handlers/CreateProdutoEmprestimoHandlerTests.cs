using Application.Commands;
using Application.Common.Interfaces;
using Application.Handlers;
using Moq;

namespace UnitTests.Application.Handlers
{
    public class CreateProdutoEmprestimoHandlerTests
    {
        private readonly Mock<IProdutoEmprestimoRepository> _repositoryMock;
        private readonly CreateProdutoEmprestimoHandler _handler;

        public CreateProdutoEmprestimoHandlerTests()
        {
            _repositoryMock = new Mock<IProdutoEmprestimoRepository>();
            _handler = new CreateProdutoEmprestimoHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleDeveChamarRepositoryAddUmaVez()
        {
            // Arrange
            var command = new CreateProdutoEmprestimoCommand("Empréstimo Pessoal", 0.18m, 60);
            _repositoryMock
                .Setup(r => r.Add(command.Nome, command.TaxaJurosAnual, command.PrazoMaximoMeses))
                .Returns(1);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Add(command.Nome, command.TaxaJurosAnual, command.PrazoMaximoMeses), Times.Once);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task HandleDeveRetornarResponseComDadosDoRequest()
        {
            // Arrange
            var command = new CreateProdutoEmprestimoCommand("Consignado", 0.12m, 48);
            _repositoryMock
                .Setup(r => r.Add(command.Nome, command.TaxaJurosAnual, command.PrazoMaximoMeses))
                .Returns(99);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(99, response.Id);
            Assert.Equal("Consignado", response.Nome);
            Assert.Equal(0.12m, response.TaxaJurosAnual);
            Assert.Equal((short)48, response.PrazoMaximoMeses);
        }
    }
}
