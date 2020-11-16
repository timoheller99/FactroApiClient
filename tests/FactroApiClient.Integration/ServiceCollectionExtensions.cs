namespace FactroApiClient.Integration
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using Serilog;

    public static class ServiceCollectionExtensions
    {
        public static void AddSerilogServices(this IServiceCollection services, LoggerConfiguration configuration)
        {
            services.AddLogging(builder => builder.AddSerilog());

            Log.Logger = configuration.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            services.AddSingleton(Log.Logger);
        }

        public static void RegisterServices(this IServiceCollection services)
        {
        }
    }
}
