namespace FactroApiClient.IntegrationTests
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    public class BaseTestFixture
    {
        public const string ValidEmployeeId = "5d82d596-308e-4df1-b199-9b62ae7458f2";

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
