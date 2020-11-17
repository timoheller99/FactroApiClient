namespace FactroApiClient.IntegrationTests.Setup
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    public class BaseTestFixture
    {
        private readonly IServiceProvider serviceProvider;

        public BaseTestFixture()
        {
            this.serviceProvider = Startup.ConfigureServices(new ServiceCollection()).BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return this.serviceProvider.GetRequiredService<T>();
        }
    }
}
