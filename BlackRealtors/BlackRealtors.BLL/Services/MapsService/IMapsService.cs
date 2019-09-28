using System.Collections.Generic;
using System.Threading.Tasks;

using BlackRealtors.Core.Models;

namespace BlackRealtors.BLL.Services.MapsService
{
    public interface IMapsService
    {
        Task<IEnumerable<Coordinates>> SearchOrganizationsByTypeAsync(
            string organizationType,
            string city
        );
    }
}
