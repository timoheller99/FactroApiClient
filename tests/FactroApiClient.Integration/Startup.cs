namespace FactroApiClient.Integration
{
    using System;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.SystemConsole.Themes;

    public static class Startup
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            });

            var loggerConfig = GetLoggerConfiguration();

            services.AddSerilogServices(loggerConfig);

            if (Log.IsEnabled(LogEventLevel.Verbose))
            {
                Log.Verbose("Verbose logging enabled.");
            }

            RegisterServices(services);

            return services;
        }

        private static LoggerConfiguration GetLoggerConfiguration()
        {
            var dateTimeString = $"{DateTime.Now.ToLongDateString()}";
            var logFilePath = $"HellCi-{dateTimeString}.log";

            const string logMessageFormatting = "[{Timestamp:HH:mm:ss} {Level} {SourceContext}]: {Message:lj}{Exception}{NewLine}";
            var config = new LoggerConfiguration();

            config.WriteTo.Console(outputTemplate: logMessageFormatting, theme: SystemConsoleTheme.Colored);
            config.WriteTo.File(logFilePath, outputTemplate: logMessageFormatting);

            config.MinimumLevel.Verbose();

            return config;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            var serviceStartCount = services.Count;
            Log.Debug("Starting service registration");

            services.RegisterServices();

            var serviceEndCount = services.Count;

            var serviceCount = serviceEndCount - serviceStartCount;

            if (serviceCount > 0)
            {
                Log.Debug("{ServiceCount} Service(s) registered successfully", serviceCount);
            }
            else
            {
                Log.Debug("No services to register.");
            }
        }
    }
}
