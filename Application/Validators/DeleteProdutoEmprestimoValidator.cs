using Application.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class DeleteProdutoEmprestimoValidator : AbstractValidator<DeleteProdutoEmprestimoCommand>
    {
        public DeleteProdutoEmprestimoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id do produto a ser deletado deve ser preenchido.")
                .GreaterThan(0).WithMessage("O Id do produto a ser deletado deve ser maior do que 0.");
        }
    }
}
