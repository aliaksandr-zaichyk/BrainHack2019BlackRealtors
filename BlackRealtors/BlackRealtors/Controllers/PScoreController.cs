using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using BlackRealtors.Api.Models.Request;
using BlackRealtors.Api.Models.Response;
using BlackRealtors.BLL.Models;
using BlackRealtors.BLL.Services.PScoreService;

using Microsoft.AspNetCore.Mvc;

namespace BlackRealtors.Api.Controllers
{
    [ApiController]
    public class PScoreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPScoreService _pScoreService;

        public PScoreController(IPScoreService pScoreService, IMapper mapper)
        {
            _pScoreService = pScoreService;
            _mapper = mapper;
        }

        [HttpPost("/api/pscore")]
        public async Task<IActionResult> CalculateScoreAsync(
            [FromBody] CalculatePScoreViewModel calculatePScoreViewModel
        )
        {
            var coordinatesModel = await _pScoreService.CalculatePScoreAsync(
                _mapper.Map<IEnumerable<OrganizationsFilterModel>>(
                    calculatePScoreViewModel.DefaultFilters
                ),
                calculatePScoreViewModel.CustomPoints
            );

            if (coordinatesModel == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<IEnumerable<WeightedCoordinatesViewModel>>(coordinatesModel));
        }
    }
}
