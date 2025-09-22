using Application.Queries;
using FluentValidation;

namespace Application.Validators
{
    public class GetProdutoEmprestimoByIdValidator : AbstractValidator<GetProdutoEmprestimoByIdQuery>
    {
        public GetProdutoEmprestimoByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id do produto a ser obtido deve ser preenchido.")
                .GreaterThan(0).WithMessage("O Id do produto a ser obtido deve ser maior do que 0.");
        }
    }
}
