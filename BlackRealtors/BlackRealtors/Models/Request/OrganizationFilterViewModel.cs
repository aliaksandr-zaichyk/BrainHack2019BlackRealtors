using BlackRealtors.Core.Constants;

namespace BlackRealtors.Api.Models.Request
{
    public class OrganizationFilterViewModel
    {
        public string OrganizationType { get; set; }
        public ImportanceLevel ImportanceLevel { get; set; }
    }
}
