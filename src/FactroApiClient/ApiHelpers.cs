namespace FactroApiClient
{
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;

    public static class ApiHelpers
    {
        public static StringContent GetStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}
