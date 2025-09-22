using Application.Commands;

namespace UnitTests.Application.Commands
{
    public class CreateProdutoEmprestimoCommandTests
    {
        [Fact]
        public void ConstrutorDeveAtribuirValoresCorretamente()
        {
            // Arrange
            string nome = "Emprestimo Pessoal";
            decimal taxaJurosAnual = 0.18m;
            short prazoMaximoMeses = 24;
            // Act
            var command = new CreateProdutoEmprestimoCommand(nome, taxaJurosAnual, prazoMaximoMeses);
            // Assert
            Assert.Equal(nome, command.Nome);
            Assert.Equal(taxaJurosAnual, command.TaxaJurosAnual);
            Assert.Equal(prazoMaximoMeses, command.PrazoMaximoMeses);
            Assert.IsType<CreateProdutoEmprestimoCommand>(command);
            Assert.IsType<string>(command.Nome);
            Assert.IsType<decimal>(command.TaxaJurosAnual);
            Assert.IsType<short>(command.PrazoMaximoMeses);
        }
    }
}
