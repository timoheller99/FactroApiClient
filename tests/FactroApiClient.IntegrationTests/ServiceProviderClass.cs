namespace FactroApiClient.IntegrationTests
{
    using FactroApiClient.IntegrationTests.Setup;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceProviderClass
    {
        static ServiceProviderClass()
        {
            ServiceProvider = Startup.ConfigureServices(new ServiceCollection()).BuildServiceProvider();
        }

        public static ServiceProvider ServiceProvider { get; }
    }
}
