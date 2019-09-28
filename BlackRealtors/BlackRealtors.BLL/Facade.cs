using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

using BlackRealtors.BLL.Services.MapsService;

using Unity;

namespace BlackRealtors.BLL
{
    public static class Facade
    {
        public static void RegisterDependencies(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IMapsService, YandexMapsService>();
        }

        public static void RegisterMappings(IMapperConfigurationExpression config)
        {
            
        }
    }
}
