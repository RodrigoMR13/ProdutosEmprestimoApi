using Domain.Entities;

namespace UnitTests.Domain.Entities
{
    public class ProdutoEmprestimoTests
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
            var produtoEmprestimo = new ProdutoEmprestimo(id, nome, taxaJurosAnual, prazoMaximoMeses);

            // Assert
            Assert.Equal(id, produtoEmprestimo.Id);
            Assert.Equal(nome, produtoEmprestimo.Nome);
            Assert.Equal(taxaJurosAnual, produtoEmprestimo.TaxaJurosAnual);
            Assert.Equal(prazoMaximoMeses, produtoEmprestimo.PrazoMaximoMeses);
            Assert.IsType<ProdutoEmprestimo>(produtoEmprestimo);
            Assert.IsType<long>(produtoEmprestimo.Id);
            Assert.IsType<string>(produtoEmprestimo.Nome);
            Assert.IsType<decimal>(produtoEmprestimo.TaxaJurosAnual);
            Assert.IsType<short>(produtoEmprestimo.PrazoMaximoMeses);
        }
    }
}
