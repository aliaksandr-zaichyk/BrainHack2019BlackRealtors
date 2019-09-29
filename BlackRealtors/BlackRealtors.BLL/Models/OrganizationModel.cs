using BlackRealtors.Core.Models;

namespace BlackRealtors.BLL.Models
{
    public class OrganizationModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Phone { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
