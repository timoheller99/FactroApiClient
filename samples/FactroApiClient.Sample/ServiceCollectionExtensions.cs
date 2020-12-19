namespace FactroApiClient.Sample
{
    using System;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Serilog;

    public static class ServiceCollectionExtensions
    {
        public static void AddSerilogServices(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            var loggingConfig = new LoggerConfiguration().ReadFrom.Configuration(configurationRoot);

            services.AddLogging(builder => builder.AddSerilog());

            Log.Logger = loggingConfig.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            services.AddSingleton(Log.Logger);
        }

        public static void RegisterServices(this IServiceCollection services)
        {
        }
    }
}
