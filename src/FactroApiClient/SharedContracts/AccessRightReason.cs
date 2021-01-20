namespace FactroApiClient.SharedContracts
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class AccessRightReason
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public AccessReason Reason { get; set; }

        public string TaskId { get; set; }

        public string PackageId { get; set; }

        public string ProjectId { get; set; }
    }
}
