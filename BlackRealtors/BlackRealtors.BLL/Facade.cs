using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

using BlackRealtors.BLL.Services.MapsService;
using BlackRealtors.BLL.Services.PScoreService;

using Unity;

namespace BlackRealtors.BLL
{
    public static class Facade
    {
        public static void RegisterDependencies(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IMapsService, YandexMapsService>();
            unityContainer.RegisterType<IPScoreService, PScoreService>();
        }

        public static void RegisterMappings(IMapperConfigurationExpression config)
        {
            
        }
    }
}
