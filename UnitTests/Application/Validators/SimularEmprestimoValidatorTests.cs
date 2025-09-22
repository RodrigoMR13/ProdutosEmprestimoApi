using Application.Commands;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Validators
{
    public class SimularEmprestimoValidatorTests
    {
        private readonly SimularEmprestimoValidator _validator;

        public SimularEmprestimoValidatorTests()
        {
            _validator = new SimularEmprestimoValidator();
        }

        [Fact]
        public void QuandoIdProdutoForVazioDeveRetornarErro()
        {
            var command = new SimularEmprestimoCommand 
            {
                ValorSolicitado = 10000.00m,
                PrazoMeses = 12
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.IdProduto)
                  .WithErrorMessage("O id do produto deve ser preenchido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void QuandoIdProdutoForMenorOuIgualAZeroDeveRetornarErro(int idProduto)
        {
            var command = new SimularEmprestimoCommand(idProduto, 10000.00m, 12);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.IdProduto)
                  .WithErrorMessage("O id do produto deve ser maior do que 0.");
        }

        [Fact]
        public void QuandoIdProdutoForValidoNaoDeveRetornarErro()
        {
            var command = new SimularEmprestimoCommand(1, 10000.00m, 12);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.IdProduto);
        }

        [Fact]
        public void QuandoValorSolicitadoForVazioDeveRetornarErro()
        {
            var command = new SimularEmprestimoCommand 
            {
                IdProduto = 1,
                PrazoMeses = 12
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ValorSolicitado)
                  .WithErrorMessage("O valor solicitado deve ser preenchido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        [InlineData(100)]
        public void QuandoValorSolicitadoForMenorOuIgualA100DeveRetornarErro(decimal valor)
        {
            var command = new SimularEmprestimoCommand(1, valor, 12);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ValorSolicitado)
                  .WithErrorMessage("O valor solicitado deve ser maior do que 100,00.");
        }

        [Fact]
        public void QuandoValorSolicitadoForValidoNaoDeveRetornarErro()
        {
            var command = new SimularEmprestimoCommand(1, 10000.00m, 12);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.ValorSolicitado);
        }

        [Fact]
        public void QuandoPrazoMesesForVazioDeveRetornarErro()
        {
            var command = new SimularEmprestimoCommand 
            { 
                IdProduto = 1,
                ValorSolicitado = 10000.00m
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PrazoMeses)
                  .WithErrorMessage("O prazo em meses deve ser preenchido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        public void QuandoPrazoMesesForMenorOuIgualAUmDeveRetornarErro(short prazoMeses)
        {
            var command = new SimularEmprestimoCommand(1, 10000.00m, prazoMeses);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PrazoMeses)
                  .WithErrorMessage("O prazo em meses deve ser maior do que 1.");
        }

        [Fact]
        public void QuandoPrazoMesesForValidoNaoDeveRetornarErro()
        {
            var command = new SimularEmprestimoCommand(1, 10000.00m, 12);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PrazoMeses);
        }
    }
}
