using Application.Queries;

namespace UnitTests.Application.Queries
{
    public class GetProdutoEmprestimoByIdTests
    {
        [Fact]
        public void ConstrutorDeveAtribuirValoresCorretamente()
        {
            // Arrange
            long id = 1L;

            // Act
            GetProdutoEmprestimoByIdQuery query = new(id);

            // Assert
            Assert.Equal(id, query.Id);
            Assert.IsType<GetProdutoEmprestimoByIdQuery>(query);
            Assert.IsType<long>(query.Id);
        }
    }
}
