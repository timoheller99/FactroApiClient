namespace FactroApiClient.Endpoints.PackageApi
{
    internal static partial class PackageApiEndpoints
    {
        public static class ShiftTasksOfPackage
        {
            public static string ShiftTasksWithSuccessors(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/shift_with_successors";
            }
        }
    }
}
