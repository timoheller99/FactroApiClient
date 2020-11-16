namespace FactroApiClient.Integration
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using Serilog;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var serviceCollection = new ServiceCollection();

                var serviceProvider = Startup.ConfigureServices(serviceCollection).BuildServiceProvider();

                var testService = serviceProvider.GetRequiredService<TestService>();

                testService.TestMethod();
            }
            catch (Exception ex)
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("./logs/crash/crash.log")
                    .WriteTo.Console()
                    .CreateLogger();

                Log.Fatal(ex, "Software terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
