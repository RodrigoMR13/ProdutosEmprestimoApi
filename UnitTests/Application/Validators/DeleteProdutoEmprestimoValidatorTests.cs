using Application.Commands;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Validators
{
    public class DeleteProdutoEmprestimoValidatorTests
    {
        private readonly DeleteProdutoEmprestimoValidator _validator;

        public DeleteProdutoEmprestimoValidatorTests()
        {
            _validator = new DeleteProdutoEmprestimoValidator();
        }

        [Fact]
        public void QuandoIdForVazioDeveRetornarErro()
        {
            var model = new DeleteProdutoEmprestimoCommand();
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("O Id do produto a ser deletado deve ser preenchido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void QuandoIdForMenorOuIgualAZeroDeveRetornarErro(long id)
        {
            var model = new DeleteProdutoEmprestimoCommand(id);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("O Id do produto a ser deletado deve ser maior do que 0.");
        }

        [Fact]
        public void QuandoIdForValidoNaoDeveRetornarErro()
        {
            var model = new DeleteProdutoEmprestimoCommand(2L);
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
