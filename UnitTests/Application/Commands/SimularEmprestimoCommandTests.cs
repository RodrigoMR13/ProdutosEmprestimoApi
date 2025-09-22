using Application.Commands;

namespace UnitTests.Application.Commands
{
    public class SimularEmprestimoCommandTests
    {
        [Fact]
        public void ConstrutorDeveAtribuirValoresCorretamente()
        {
            // Arrange
            long id = 1L;
            decimal valorSolicitado = 10000.00m;
            short prazoMeses = 12;
            // Act
            var command = new SimularEmprestimoCommand(id, valorSolicitado, prazoMeses);
            // Assert
            Assert.Equal(id, command.IdProduto);
            Assert.Equal(valorSolicitado, command.ValorSolicitado);
            Assert.Equal(prazoMeses, command.PrazoMeses);
            Assert.IsType<SimularEmprestimoCommand>(command);
            Assert.IsType<long>(command.IdProduto);
            Assert.IsType<decimal>(command.ValorSolicitado);
            Assert.IsType<short>(command.PrazoMeses);
        }
    }
}
