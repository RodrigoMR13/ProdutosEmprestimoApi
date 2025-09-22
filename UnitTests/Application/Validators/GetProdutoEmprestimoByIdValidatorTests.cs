using Application.Queries;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Validators
{
    public class GetProdutoEmprestimoByIdValidatorTests
    {
        private readonly GetProdutoEmprestimoByIdValidator _validator;

        public GetProdutoEmprestimoByIdValidatorTests()
        {
            _validator = new GetProdutoEmprestimoByIdValidator();
        }

        [Fact]
        public void QuandoIdForVazioDeveRetornarErro()
        {
            var query = new GetProdutoEmprestimoByIdQuery { Id = 0 };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("O Id do produto a ser obtido deve ser preenchido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void QuandoIdForMenorOuIgualAZeroDeveRetornarErro(long id)
        {
            var query = new GetProdutoEmprestimoByIdQuery { Id = id };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("O Id do produto a ser obtido deve ser maior do que 0.");
        }

        [Fact]
        public void QuandoIdForValidoNaoDeveRetornarErro()
        {
            var query = new GetProdutoEmprestimoByIdQuery { Id = 1 };
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
