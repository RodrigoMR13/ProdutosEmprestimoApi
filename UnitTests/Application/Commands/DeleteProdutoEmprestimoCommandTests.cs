using Application.Commands;

namespace UnitTests.Application.Commands
{
    public class DeleteProdutoEmprestimoCommandTests
    {
        [Fact]
        public void ConstrutorDeveAtribuirValoresCorretamente()
        {
            // Arrange
            long id = 1L;
            // Act
            var command = new DeleteProdutoEmprestimoCommand(id);
            // Assert
            Assert.Equal(id, command.Id);
            Assert.IsType<DeleteProdutoEmprestimoCommand>(command);
            Assert.IsType<long>(command.Id);
        }
    }
}
