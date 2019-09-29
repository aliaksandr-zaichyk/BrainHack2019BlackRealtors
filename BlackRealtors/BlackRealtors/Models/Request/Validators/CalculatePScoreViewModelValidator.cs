using System.Linq;

using FluentValidation;

namespace BlackRealtors.Api.Models.Request.Validators
{
    public class CalculatePScoreViewModelValidator : AbstractValidator<CalculatePScoreViewModel>
    {
        public CalculatePScoreViewModelValidator()
        {
            RuleForEach(x => x.DefaultFilters)
                .SetValidator(new OrganizationFilterViewModelValidator());
        }
    }
}
