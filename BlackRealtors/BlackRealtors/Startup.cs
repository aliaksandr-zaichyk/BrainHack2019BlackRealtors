using AutoMapper;

using BlackRealtors.Api.Models.Request;
using BlackRealtors.Api.Models.Response;
using BlackRealtors.BLL;
using BlackRealtors.BLL.Models;
using BlackRealtors.Core.Configurations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Newtonsoft.Json.Converters;

using Unity;

namespace BlackRealtors.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(
                        opt =>
                        {
                            opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                        }
                    );

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(
                configuration => { configuration.RootPath = "ClientApp/build"; }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc();

            app.UseSpa(
                spa =>
                {
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseReactDevelopmentServer(npmScript: "start");
                    }
                }
            );
        }

        private void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterInstance(SetupMapper());

            var yandexMapsApiConfig = new YandexMapsApiConfiguration();
            Configuration.GetSection("YandexMapsApiConfiguration").Bind(yandexMapsApiConfig);
            container.RegisterInstance(Options.Create(yandexMapsApiConfig));

            var pScoreConfiguration = new PScoreConfiguration();
            Configuration.GetSection("PScoreConfiguration").Bind(pScoreConfiguration);
            container.RegisterInstance(Options.Create(pScoreConfiguration));

            Facade.RegisterDependencies(container);
        }

        private IMapper SetupMapper()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    Facade.RegisterMappings(cfg);

                    cfg.CreateMap<OrganizationsFilterViewModel, OrganizationsFilterModel>()
                       .ReverseMap();

                    cfg.CreateMap<WeightedCoordinatesViewModel, WeightedCoordinatesModel>()
                       .ReverseMap();
                }
            );

            return new Mapper(config);
        }
    }
}
