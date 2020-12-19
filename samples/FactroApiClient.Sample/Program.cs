namespace FactroApiClient.Sample
{
    using System;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.CompanyTag;

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

                var companyApi = serviceProvider.GetRequiredService<ICompanyApi>();

                var createdTag = companyApi.CreateCompanyTagAsync(new CreateCompanyTagRequest("AUTOTEST_Tag")).GetAwaiter().GetResult();
                companyApi.DeleteCompanyTagAsync(createdTag.Id).GetAwaiter();
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
