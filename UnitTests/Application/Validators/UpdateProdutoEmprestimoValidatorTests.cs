using Application.Commands;
using Application.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Validators
{
    public class UpdateProdutoEmprestimoValidatorTests
    {
        private readonly UpdateProdutoEmprestimoValidator _validator;

        public UpdateProdutoEmprestimoValidatorTests()
        {
            _validator = new UpdateProdutoEmprestimoValidator();
        }

        [Fact]
        public void QuandoIdForVazioDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand
            {
                Nome = "Produto Teste",
                TaxaJurosAnual = 0.10m,
                PrazoMaximoMeses = 24,
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("O Id do produto a ser atualizado deve ser preenchido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void QuandoIdForMenorOuIgualAZeroDeveRetornarErro(int id)
        {
            var command = new UpdateProdutoEmprestimoCommand(id, "Produto Teste", 0.10m, 24);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("O Id do produto a ser atualizado deve ser maior do que 0.");
        }

        [Fact]
        public void QuandoIdForValidoNaoDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand(1, "Produto Teste", 0.10m, 24);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void QuandoNomeForVazioDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand
            {
                Id = 1,
                TaxaJurosAnual = 0.10m,
                PrazoMaximoMeses = 24
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                  .WithErrorMessage("O nome do produto deve ser preenchido.");
        }

        [Fact]
        public void QuandoNomeExcederOTamanhoMaximoDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand(1, new string('A', 121), 0.10m, 24);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                  .WithErrorMessage("O nome do produto deve ter no máximo 120 caracteres.");
        }

        [Fact]
        public void QuandoNomeForValidoNaoDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand(1, "Produto Teste", 0.10m, 24);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void QuandoTaxaJurosAnualForVaziaDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand 
            {
                Id = 1,
                Nome = "Produto Teste",
                PrazoMaximoMeses = 24
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.TaxaJurosAnual)
                  .WithErrorMessage("A taxa de juros anual deve ser preenchida.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void QuandoTaxaJurosAnualForMenorOuIgualAZeroDeveRetornarErro(decimal taxa)
        {
            var command = new UpdateProdutoEmprestimoCommand(1, "Produto Teste", taxa, 24);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.TaxaJurosAnual)
                  .WithErrorMessage("A taxa de juros anual deve ser maior do que 0.");
        }

        [Fact]
        public void QuandoTaxaJurosAnualForValidaNaoDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand(1, "Produto Teste", 0.10m, 24);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.TaxaJurosAnual);
        }

        [Fact]
        public void QuandoPrazoMaximoMesesForVazioDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand
            {
                Id = 1,
                Nome = "Produto Teste",
                TaxaJurosAnual = 0.10m
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PrazoMaximoMeses)
                  .WithErrorMessage("O prazo máximo em meses deve ser preenchido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        public void QuandoPrazoMaximoMesesForMenorOuIgualAUmDeveRetornarErro(short prazo)
        {
            var command = new UpdateProdutoEmprestimoCommand(1, "Produto Teste", 0.10m, prazo);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PrazoMaximoMeses)
                  .WithErrorMessage("O prazo máximo em meses deve ser maior do que 1.");
        }

        [Fact]
        public void QuandoPrazoMaximoMesesForValidoNaoDeveRetornarErro()
        {
            var command = new UpdateProdutoEmprestimoCommand(1, "Produto Teste", 0.10m, 24);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.PrazoMaximoMeses);
        }
    }
}
