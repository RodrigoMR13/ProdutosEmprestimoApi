using Application.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateProdutoEmprestimoValidator : AbstractValidator<UpdateProdutoEmprestimoCommand>
    {
        public UpdateProdutoEmprestimoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id do produto a ser atualizado deve ser preenchido.")
                .GreaterThan(0).WithMessage("O Id do produto a ser atualizado deve ser maior do que 0.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do produto deve ser preenchido.")
                .MaximumLength(120).WithMessage("O nome do produto deve ter no máximo 120 caracteres.");

            RuleFor(x => x.TaxaJurosAnual)
                .NotEmpty().WithMessage("A taxa de juros anual deve ser preenchida.")
                .GreaterThan(0).WithMessage("A taxa de juros anual deve ser maior do que 0.");

            RuleFor(x => x.PrazoMaximoMeses)
                .NotEmpty().WithMessage("O prazo máximo em meses deve ser preenchido.")
                .GreaterThan((short)1).WithMessage("O prazo máximo em meses deve ser maior do que 1.");
        }
    }
}
