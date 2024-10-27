using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Commands
{
    public class UpdateShoppingCartCommandValidator : AbstractValidator<UpdateShoppingCartCommand>
    {

        public UpdateShoppingCartCommandValidator()
        {
            RuleFor(s => s.CreatedAt).NotEmpty();
            RuleFor(s => s.Name).NotEmpty().MaximumLength(100);
            RuleFor(s => s.TotalItems).GreaterThan(0);
            RuleFor(s => s.TotalPrice).GreaterThan(0);
            RuleFor(s => s.Id).NotEmpty().Must(BeAValidGuid).WithMessage("Must be a valid guid");
        }

        private bool BeAValidGuid(Guid guid)
        {
            return Guid.TryParse(guid.ToString(), out _);
        }
    }
}
