namespace FactroApiClient
{
    using System;
    using System.Net.Http.Headers;
    using System.Net.Mime;

    using FactroApiClient.Appointment;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Http;

    public static class ServiceCollectionExtensions
    {
        public static void AddFactroApiClientServices(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            serviceCollection.RegisterHttpClient(configurationRoot);

            serviceCollection.AddTransient<IAppointmentApi, AppointmentApi>();
        }

        private static void RegisterHttpClient(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            var configSection = configurationRoot.GetSection("FactroApiClient").GetSection("ApiToken");
            var apiToken = configSection.Value;
            if (apiToken == null)
            {
                throw new Exception($"Could not find Factro API token in '{configSection.Path}'.");
            }

            serviceCollection.AddHttpClient(
                "BaseClient",
                (provider, client) =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiToken);
                client.BaseAddress = new Uri("https://cloud.factro.com/api/core/");
            });
            serviceCollection.RemoveAll<IHttpMessageHandlerBuilderFilter>();
        }
    }
}
