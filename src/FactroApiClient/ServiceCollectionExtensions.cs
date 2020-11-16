namespace FactroApiClient
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddFactroApiClientServices(this IServiceCollection serviceCollection)
        {
            // serviceCollection.AddTransient<ITestService, TestService>();
        }
    }
}
