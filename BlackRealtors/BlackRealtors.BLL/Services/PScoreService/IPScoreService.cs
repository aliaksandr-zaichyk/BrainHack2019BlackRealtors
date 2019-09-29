using System.Collections.Generic;
using System.Threading.Tasks;

using BlackRealtors.BLL.Models;
using BlackRealtors.Core.Models;

namespace BlackRealtors.BLL.Services.PScoreService
{
    public interface IPScoreService
    {
        Task<WeightedCoordinatesModel> CalculatePScoreAsync(
            IEnumerable<OrganizationsFilterModel> filters,
            IEnumerable<Coordinates> customPoints
        );
    }
}
