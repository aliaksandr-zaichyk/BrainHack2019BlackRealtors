using FluentValidation;

namespace BlackRealtors.Api.Models.Request.Validators
{
    public class OrganizationFilterViewModelValidator
        : AbstractValidator<OrganizationFilterViewModel>
    {
        public OrganizationFilterViewModelValidator()
        {
            RuleFor(x => x.OrganizationType)
                .NotEmpty();
        }
    }
}
