using Application.Queries;
using System.Reflection;

namespace UnitTests.Application.Queries
{
    public class GetProdutosEmprestimoQueryTests
    {
        [Fact]
        public void ConstrutorDeveCriarObjetoValido()
        {
            // Arrange
            // Act
            GetProdutosEmprestimoQuery query = new();

            // Assert
            Assert.IsType<GetProdutosEmprestimoQuery>(query);
            Assert.NotNull(query);
        }

        [Fact]
        public void ConstrutorDeveCriarObjetoSemPropriedades()
        {
            // Arrange
            Type type = typeof(GetProdutosEmprestimoQuery);

            // Act
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Assert
            Assert.Empty(properties);
        }
    }
}
