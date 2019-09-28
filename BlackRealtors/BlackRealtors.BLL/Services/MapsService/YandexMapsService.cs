﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<IEnumerable<Coordinates>> SearchOrganizationsByTypeAsync(
            string organizationType
        )
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if (!OrganizationType.ValidOrganizationType(organizationType))
                    {
                        return Enumerable.Empty<Coordinates>();
                    }

                    var responseMessage =
                        await client.GetAsync(
                            $"{GetOrganizationsApi}?" +
                            $"apikey={HttpUtility.UrlEncode(_yandexMapsApiConfiguration.ApiKey)}" +
                            $"&lang={HttpUtility.UrlEncode(_yandexMapsApiConfiguration.Language)}" +
                            $"&text={HttpUtility.UrlEncode(organizationType + " " + Cities.Hrodna)}" +
                            $"&results=500"
                        );

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        return Enumerable.Empty<Coordinates>();
                    }

                    var response = await responseMessage.Content.ReadAsStringAsync();
                    var parsedResponse = JObject.Parse(response);

                    var features = parsedResponse.SelectToken("features");

                    var coordinates = new List<Coordinates>();

                    foreach (var feature in features)
                    {
                        var jsonCoordinatesLong = feature.SelectToken("geometry.coordinates[0]");
                        var jsonCoordinatesLat = feature.SelectToken("geometry.coordinates[1]");

                        coordinates.Add(
                            new Coordinates
                            {
                                Longitude = jsonCoordinatesLong.Value<double>(),
                                Latitude = jsonCoordinatesLat.Value<double>()
                            }
                        );
                    }

                    return coordinates;
                }
                catch
                {
                    return Enumerable.Empty<Coordinates>();
                }
            }
        }
    }
}
