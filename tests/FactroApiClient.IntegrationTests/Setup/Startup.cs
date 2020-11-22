namespace FactroApiClient.IntegrationTests.Setup
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Serilog;
    using Serilog.Sinks.SystemConsole.Themes;

    public static class Startup
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.ClearProviders());

            var loggerConfig = GetLoggerConfiguration();

            services.AddSerilogServices(loggerConfig);

            var configurationRoot = CreateConfigurationRoot();
            services.AddSingleton(configurationRoot);

            services.AddFactroApiClientServices(configurationRoot);
            return services;
        }

        private static LoggerConfiguration GetLoggerConfiguration()
        {
            const string logMessageFormatting = "[{Timestamp:HH:mm:ss} {Level} {SourceContext}]: {Message:lj}{Exception}{NewLine}";
            var config = new LoggerConfiguration();

            config.WriteTo.Console(outputTemplate: logMessageFormatting, theme: SystemConsoleTheme.Colored);

            config.MinimumLevel.Verbose();

            return config;
        }

        private static IConfigurationRoot CreateConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
