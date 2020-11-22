namespace FactroApiClient.UnitTests
{
    using Newtonsoft.Json;

    public class BaseTestFixture
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = new CamelCaseContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        };
    }
}
