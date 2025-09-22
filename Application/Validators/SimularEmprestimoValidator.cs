using Application.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class SimularEmprestimoValidator : AbstractValidator<SimularEmprestimoCommand>
    {
        public SimularEmprestimoValidator()
        {
            RuleFor(x => x.IdProduto)
                .NotEmpty().WithMessage("O id do produto deve ser preenchido.")
                .GreaterThan(0).WithMessage("O id do produto deve ser maior do que 0.");

            RuleFor(x => x.ValorSolicitado)
                .NotEmpty().WithMessage("O valor solicitado deve ser preenchido.")
                .GreaterThan(100.00m).WithMessage("O valor solicitado deve ser maior do que 100,00.");

            RuleFor(x => x.PrazoMeses)
                .NotEmpty().WithMessage("O prazo em meses deve ser preenchido.")
                .GreaterThan((short)1).WithMessage("O prazo em meses deve ser maior do que 1.");
        }
    }
}
