using CustomerAPI.Models;
using FluentValidation;

namespace CustomerAPI.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(command => command.Age).GreaterThan(18);
            RuleFor(command => command.FirstName).NotEmpty();
            RuleFor(command => command.LastName);
            RuleFor(command => command.ID).NotEmpty();
        }
    }
}
