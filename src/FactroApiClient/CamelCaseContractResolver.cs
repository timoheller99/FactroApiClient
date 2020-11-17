namespace FactroApiClient
{
    using System.Linq;

    using Newtonsoft.Json.Serialization;

    using static System.Char;

    public class CamelCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return this.ToCamelCase(propertyName);
        }

        private string ToCamelCase(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || IsLower(str, 0))
            {
                return str;
            }

            return $"{ToLowerInvariant(str.First())}{str.Substring(1)}";
        }
    }
}
