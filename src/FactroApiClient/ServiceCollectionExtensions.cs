namespace FactroApiClient
{
    using System;
    using System.Net.Http.Headers;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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

            serviceCollection.AddHttpClient(
                "BaseClient",
                (provider, client) =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authorization", apiToken);
                client.BaseAddress = new Uri("https://cloud.factro.com/api/core");
            });
        }
    }
}
