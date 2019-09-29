using System.Collections.Generic;

using BlackRealtors.Core.Constants;

namespace BlackRealtors.BLL.Models
{
    public class PScoreModel
    {
        public string OrganizationType { get; set; }
        public ImportanceLevel ImportanceLevel { get; set; }
        public IEnumerable<OrganizationModel> Organizations { get; set; }
    }
}
