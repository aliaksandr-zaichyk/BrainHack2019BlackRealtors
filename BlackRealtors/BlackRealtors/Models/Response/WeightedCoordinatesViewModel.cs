using BlackRealtors.Core.Models;

namespace BlackRealtors.Api.Models.Response
{
    public class WeightedCoordinatesViewModel
    {
        public Coordinates Coordinates { get; set; }
        public double Weight { get; set; }
    }
}
