using System.Collections.Generic;

using BlackRealtors.Core.Models;

namespace BlackRealtors.Api.Models.Request
{
    public class CalculatePScoreViewModel
    {
        public IEnumerable<OrganizationsFilterViewModel> DefaultFilters { get; set; }
        public IEnumerable<Coordinates> CustomPoints { get; set; }
    }
}
