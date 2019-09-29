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
using Newtonsoft.Json.Serialization;

namespace BlackRealtors.BLL.Services.PScoreService
{
    public class PScoreService : IPScoreService
    {
        public const string PostPScoreApi = "/pscore";
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

        public async Task<IEnumerable<WeightedCoordinatesModel>> CalculatePScoreAsync(
            IEnumerable<OrganizationsFilterModel> filters,
            IEnumerable<Coordinates> customPoints
        )
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var pScoreModels = new List<PScoreModel>();

                    if (filters != null)
                    {
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
                    }

                    pScoreModels.AddRange(GetNotSelectedOrganizationTypes(pScoreModels));

                    if (customPoints != null)
                    {
                        pScoreModels.Add(
                            new PScoreModel
                            {
                                OrganizationType = string.Empty,
                                ImportanceLevel = ImportanceLevel.High,
                                Organizations = customPoints.Select(
                                    x => new OrganizationModel
                                    {
                                        Coordinates = new Coordinates
                                        {
                                            Longitude = x.Longitude,
                                            Latitude = x.Latitude
                                        }
                                    }
                                )
                            }
                        );
                    }

                    var a = JsonConvert.SerializeObject(
                        pScoreModels,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }
                    );

                    var responseMessage = await client.PostAsync(
                        $"{_pScoreConfiguration.BaseUrl}{PostPScoreApi}",
                        new StringContent(
                            a,
                            Encoding.UTF8,
                            MediaTypeNames.Application.Json
                        )
                    );

                    if (!responseMessage.IsSuccessStatusCode) return null;

                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var weightedCoordinates =
                        JsonConvert.DeserializeObject<IEnumerable<WeightedCoordinatesModel>>(response,
                            new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });

                    return weightedCoordinates;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

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
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.Atm,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.BeautySalon) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.BeautySalon,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Fitness) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.Fitness,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.GroceryStore) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.GroceryStore,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Hospital) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.Hospital,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Kindergarten) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.Kindergarten,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.Pharmacy) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.Pharmacy,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.School) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.School,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(x => x.OrganizationType == OrganizationType.ShoppingMall) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.ShoppingMall,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            if (selected.FirstOrDefault(
                    x => x.OrganizationType == OrganizationType.VeterinaryClinic
                ) ==
                null)
                pScoreModels.Add(
                    new PScoreModel
                    {
                        OrganizationType = OrganizationType.VeterinaryClinic,
                        Organizations = Enumerable.Empty<OrganizationModel>()
                    }
                );

            return pScoreModels;
        }
    }
}
