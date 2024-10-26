using FluentValidation;

namespace Application.Commands
{
    public class DeleteShoppingCartByIdCommandValidator: AbstractValidator<DeleteShoppingCartByIdCommand>
    {
        public DeleteShoppingCartByIdCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
