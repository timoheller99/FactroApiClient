namespace FactroApiClient.IntegrationTests
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    public class BaseTestFixture
    {
        public const string ValidEmployeeId = "2151d5e7-4f51-4ab5-9b8f-7b9c2321bb92";

        public const string TestPrefix = "AUTOTEST_";

        public T GetService<T>()
        {
            return ServiceProviderClass.ServiceProvider.GetRequiredService<T>();
        }

        public virtual async Task ClearFactroInstanceAsync()
        {
            await Task.CompletedTask;
        }
    }
}
