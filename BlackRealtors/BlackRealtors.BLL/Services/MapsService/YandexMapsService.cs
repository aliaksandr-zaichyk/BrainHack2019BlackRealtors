using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using BlackRealtors.BLL.Models;
using BlackRealtors.Core.Configurations;
using BlackRealtors.Core.Constants;
using BlackRealtors.Core.Models;

using Microsoft.Extensions.Options;

using Newtonsoft.Json.Linq;

namespace BlackRealtors.BLL.Services.MapsService
{
    public class YandexMapsService : IMapsService
    {
        public const string GetOrganizationsApi = "https://search-maps.yandex.ru/v1/";

        private readonly YandexMapsApiConfiguration _yandexMapsApiConfiguration;

        public YandexMapsService(IOptions<YandexMapsApiConfiguration> yandexMapsApiConfig)
        {
            _yandexMapsApiConfiguration = yandexMapsApiConfig?.Value ??
                                          throw new ArgumentNullException(
                                              nameof(yandexMapsApiConfig)
                                          );
        }

        public async Task<IEnumerable<OrganizationModel>> SearchOrganizationsByTypeAsync(
            string organizationType,
            string city
        )
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if (!OrganizationType.ValidOrganizationType(organizationType) ||
                        !Cities.ValidCity(city))
                    {
                        return Enumerable.Empty<OrganizationModel>();
                    }

                    var responseMessage =
                        await client.GetAsync(
                            $"{GetOrganizationsApi}?" +
                            $"apikey={HttpUtility.UrlEncode(_yandexMapsApiConfiguration.ApiKey)}" +
                            $"&lang={HttpUtility.UrlEncode(_yandexMapsApiConfiguration.Language)}" +
                            $"&text={HttpUtility.UrlEncode(organizationType + " " + city)}" +
                            "&results=500"
                        );

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        return Enumerable.Empty<OrganizationModel>();
                    }

                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var parsedResponse = JObject.Parse(response);

                    var features = parsedResponse.SelectToken("features");

                    var organizations = new List<OrganizationModel>();

                    foreach (var feature in features)
                    {
                        var jsonCoordinatesLong = feature.SelectToken("geometry.coordinates[0]");
                        var jsonCoordinatesLat = feature.SelectToken("geometry.coordinates[1]");
                        var jsonName = feature.SelectToken("properties.CompanyMetaData.name");
                        var jsonPhone = feature.SelectToken(
                            "properties.CompanyMetaData.Phones[0].formatted"
                        );
                        var jsonUrl = feature.SelectToken("properties.CompanyMetaData.url");

                        organizations.Add(
                            new OrganizationModel
                            {
                                Name = jsonName?.Value<string>(),
                                Phone = jsonPhone?.Value<string>(),
                                Url = jsonUrl?.Value<string>(),
                                Coordinates = new Coordinates
                                {
                                    Longitude = jsonCoordinatesLong.Value<double>(),
                                    Latitude = jsonCoordinatesLat.Value<double>()
                                }
                            }
                        );
                    }

                    return organizations;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Enumerable.Empty<OrganizationModel>();
                }
            }
        }
    }
}
