using AzureBlobStorage.Interface;
using BlobOperations;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using WeatherReportDomainLayer;

namespace AzureBlobStorage
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IBlobOperations, BlobOperation>();
            container.RegisterType<IWeatherReport, WeatherReport>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}