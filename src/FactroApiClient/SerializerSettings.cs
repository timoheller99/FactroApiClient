namespace FactroApiClient
{
    using Newtonsoft.Json;

    internal static class SerializerSettings
    {
        public static JsonSerializerSettings JsonSerializerSettings { get; } = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = new CamelCaseContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        };
    }
}
