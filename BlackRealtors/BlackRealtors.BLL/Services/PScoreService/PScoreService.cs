using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using BlackRealtors.BLL.Models;
using BlackRealtors.BLL.Services.MapsService;
using BlackRealtors.Core.Configurations;
using BlackRealtors.Core.Constants;
using BlackRealtors.Core.Models;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace BlackRealtors.BLL.Services.PScoreService
{
    public class PScoreService : IPScoreService
    {
        public const string PostPizdatiyScoreApi = "/pizdatiy_score";
        private readonly PScoreConfiguration _pScoreConfiguration;
        private readonly IMapsService _mapsService;

        public PScoreService(
            IOptions<PScoreConfiguration> pScoreConfiguration,
            IMapsService mapsService
        )
        {
            _mapsService = mapsService;
            _pScoreConfiguration = pScoreConfiguration?.Value ??
                                   throw new ArgumentNullException(nameof(pScoreConfiguration));
        }

        public async Task<WeightedCoordinatesModel> CalculatePScoreAsync(
            IEnumerable<OrganizationsFilterModel> filters,
            IEnumerable<Coordinates> customPoints
        )
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var pScoreModels = new List<PScoreModel>();

                    foreach (var filter in filters)
                    {
                        if (!OrganizationType.ValidOrganizationType(filter.OrganizationType))
                            return null;

                        if (filter.ImportanceLevel != ImportanceLevel.Useless)
                        {
                            var organizations =
                                await _mapsService.SearchOrganizationsByTypeAsync(
                                    filter.OrganizationType,
                                    Cities.Hrodna
                                );

                            pScoreModels.Add(
                                new PScoreModel
                                {
                                    OrganizationType = filter.OrganizationType,
                                    ImportanceLevel = filter.ImportanceLevel,
                                    Organizations = organizations
                                }
                            );
                        }
                    }

                    pScoreModels.AddRange(GetNotSelectedOrganizationTypes(pScoreModels));

                    foreach (var coor in customPoints)
                        pScoreModels.Add(
                            new PScoreModel
                            {
                                OrganizationType = string.Empty,
                                ImportanceLevel = ImportanceLevel.High
                            }
                        );

                    var responseMessage = await client.PostAsync(
                        $"{_pScoreConfiguration.BaseUrl}{PostPizdatiyScoreApi}",
                        new StringContent(
                            JsonConvert.SerializeObject(pScoreModels),
                            Encoding.UTF8,
                            MediaTypeNames.Application.Json
                        )
                    );

                    if (!responseMessage.IsSuccessStatusCode) return null;

                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var weightedCoordinates =
                        JsonConvert.DeserializeObject<WeightedCoordinatesModel>(response);

                    return weightedCoordinates;
                }
                catch
                {
                    return null;
                }
            }
        }

        private IEnumerable<PScoreModel> GetNotSelectedOrganizationTypes(
            IEnumerable<PScoreModel> selected
        )
        {
            var pScoreModels = new List<PScoreModel>();

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Atm) == null)
                pScoreModels.Add(new PScoreModel {OrganizationType = OrganizationType.Atm});

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.BeautySalon) ==
                null)
                pScoreModels.Add(new PScoreModel {OrganizationType = OrganizationType.BeautySalon});

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Fitness) ==
                null)
                pScoreModels.Add(new PScoreModel {OrganizationType = OrganizationType.Fitness});

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.GroceryStore) ==
                null)
                pScoreModels.Add(
                    new PScoreModel {OrganizationType = OrganizationType.GroceryStore}
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Hospital) ==
                null)
                pScoreModels.Add(new PScoreModel {OrganizationType = OrganizationType.Hospital});

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Kindergarten) ==
                null)
                pScoreModels.Add(
                    new PScoreModel {OrganizationType = OrganizationType.Kindergarten}
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Pharmacy) ==
                null)
                pScoreModels.Add(new PScoreModel {OrganizationType = OrganizationType.Pharmacy});

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.School) ==
                null)
                pScoreModels.Add(new PScoreModel {OrganizationType = OrganizationType.School});

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.ShoppingMall) ==
                null)
                pScoreModels.Add(
                    new PScoreModel {OrganizationType = OrganizationType.ShoppingMall}
                );

            if (selected.FirstOrDefault(
                    x => x.OrganizationType == OrganizationType.VeterinaryClinic
                ) ==
                null)
                pScoreModels.Add(
                    new PScoreModel {OrganizationType = OrganizationType.VeterinaryClinic}
                );

            return pScoreModels;
        }
    }
}
