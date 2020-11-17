namespace FactroApiClient
{
    using System;
    using System.Net.Http.Headers;
    using System.Net.Mime;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    public static class ServiceCollectionExtensions
    {
        public static void AddFactroApiClientServices(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            serviceCollection.RegisterHttpClient(configurationRoot);

            // serviceCollection.AddTransient<ITestService, TestService>();
        }

        private static void RegisterHttpClient(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            var configSection = configurationRoot.GetSection("FactroApiClient").GetSection("ApiToken");
            var apiToken = configSection.Value;
            if (apiToken == null)
            {
                throw new Exception($"Could not find Factro API token in '{configSection.Path}'.");
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCaseContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            };

            serviceCollection.AddSingleton(jsonSerializerSettings);

            serviceCollection.AddHttpClient(
                "BaseClient",
                (provider, client) =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiToken);
            });
        }
    }
}
