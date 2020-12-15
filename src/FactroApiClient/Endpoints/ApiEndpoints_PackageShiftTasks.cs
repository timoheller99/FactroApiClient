namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class PackageShiftTasks
        {
            public static string ShiftTasksWithSuccessors(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/shift_with_successors";
            }
        }
    }
}
