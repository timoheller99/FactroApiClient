namespace FactroApiClient.IntegrationTests
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.IntegrationTests.Setup;

    using Microsoft.Extensions.DependencyInjection;

    public class BaseTestFixture
    {
        public const string ValidEmployeeId = "5d82d596-308e-4df1-b199-9b62ae7458f2";

        public const string TestPrefix = "AUTOTEST_";

        private readonly IServiceProvider serviceProvider;

        public BaseTestFixture()
        {
            this.serviceProvider = Startup.ConfigureServices(new ServiceCollection()).BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return this.serviceProvider.GetRequiredService<T>();
        }

        public virtual async Task ClearFactroInstanceAsync()
        {
            await Task.CompletedTask;
        }
    }
}
