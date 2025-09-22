using Application.Commands;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Validators
{
    public class CreateProdutoEmprestimoValidatorTests
    {
        private readonly CreateProdutoEmprestimoValidator _validator;

        public CreateProdutoEmprestimoValidatorTests()
        {
            _validator = new CreateProdutoEmprestimoValidator();
        }

        [Fact]
        public void QuandoNomeForVazioDeveRetornarErro()
        {
            var model = new CreateProdutoEmprestimoCommand("", 0.10m, 24);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                  .WithErrorMessage("O nome do produto deve ser preenchido.");
        }

        [Fact]
        public void QuandoNomeExcederOTamanhoMaximoDeveRetornarErro()
        {
            var model = new CreateProdutoEmprestimoCommand(new string('A', 121), 0.10m, 24);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                  .WithErrorMessage("O nome do produto deve ter no máximo 120 caracteres.");
        }

        [Fact]
        public void QuandoNomeForValidoNaoDeveRetornarErro()
        {
            var model = new CreateProdutoEmprestimoCommand("Produto Teste", 0.10m, 24);
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void QuandoTaxaJurosAnualForVaziaDeveRetornarErro()
        {
            var model = new CreateProdutoEmprestimoCommand
            {
                Nome = "Produto Teste",
                PrazoMaximoMeses = 24
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TaxaJurosAnual)
                  .WithErrorMessage("A taxa de juros anual deve ser preenchida.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void QuantoTaxaJurosAnualForMenorOuIgualAZeroDeveRetornarErro(decimal txJurosAnual)
        {
            var model = new CreateProdutoEmprestimoCommand("Produto Teste", txJurosAnual, 24);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TaxaJurosAnual)
                  .WithErrorMessage("A taxa de juros anual deve ser maior do que 0.");
        }

        [Fact]
        public void QuandoTaxaDeJurosForValidaNaoDeveRetornarErro()
        {
            var model = new CreateProdutoEmprestimoCommand("Produto Teste", 0.10m, 24);
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.TaxaJurosAnual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        public void QuandoPrazoMaximoMesesForMenorDoQue2DeveRetornarErro(short prazoMaxMeses)
        {
            var model = new CreateProdutoEmprestimoCommand("Produto Teste", 0.10m, prazoMaxMeses);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PrazoMaximoMeses)
                  .WithErrorMessage("O prazo máximo em meses deve ser maior do que 1.");
        }

        [Fact]
        public void QuandoPrazoMaximoMesesForValidoNaoDeveRetornarErro()
        {
            var model = new CreateProdutoEmprestimoCommand("Produto Teste", 0.10m, 24);
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.PrazoMaximoMeses);
        }
    }
}
