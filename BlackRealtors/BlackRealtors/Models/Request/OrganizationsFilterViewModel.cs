using BlackRealtors.Core.Constants;

namespace BlackRealtors.Api.Models.Request
{
    public class OrganizationsFilterViewModel
    {
        public string OrganizationType { get; set; }
        public ImportanceLevel ImportanceLevel { get; set; }
    }
}
