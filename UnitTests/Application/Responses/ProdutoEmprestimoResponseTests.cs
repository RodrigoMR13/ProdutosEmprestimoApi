using Application.Responses;

namespace UnitTests.Application.Responses
{
    public class ProdutoEmprestimoResponseTests
    {
        [Fact]
        public void ConstrutorDeveAtribuirValoresCorretamente()
        {
            // Arrange
            long id = 1L;
            string nome = "Emprestimo Pessoal";
            decimal taxaJurosAnual = 0.18m;
            short prazoMaximoMeses = 24;

            // Act
            var command = new ProdutoEmprestimoResponse(id, nome, taxaJurosAnual, prazoMaximoMeses);

            // Assert
            Assert.Equal(id, command.Id);
            Assert.Equal(nome, command.Nome);
            Assert.Equal(taxaJurosAnual, command.TaxaJurosAnual);
            Assert.Equal(prazoMaximoMeses, command.PrazoMaximoMeses);
            Assert.IsType<ProdutoEmprestimoResponse>(command);
            Assert.IsType<long>(command.Id);
            Assert.IsType<string>(command.Nome);
            Assert.IsType<decimal>(command.TaxaJurosAnual);
            Assert.IsType<short>(command.PrazoMaximoMeses);
        }
    }
}
