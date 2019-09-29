using System.Collections.Generic;
using System.Threading.Tasks;

using BlackRealtors.BLL.Models;

namespace BlackRealtors.BLL.Services.MapsService
{
    public interface IMapsService
    {
        Task<IEnumerable<OrganizationModel>> SearchOrganizationsByTypeAsync(
            string organizationType,
            string city
        );
    }
}
