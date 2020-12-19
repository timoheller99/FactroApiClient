namespace FactroApiClient.Sample
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class Startup
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.ClearProviders());

            var configurationRoot = CreateConfigurationRoot();
            services.AddSingleton(configurationRoot);

            services.AddSerilogServices(configurationRoot);

            services.RegisterServices();

            services.AddFactroApiClientServices(configurationRoot);
            return services;
        }

        private static IConfigurationRoot CreateConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
