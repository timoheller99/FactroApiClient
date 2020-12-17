namespace FactroApiClient.Package.Contracts
{
    public class ShiftPackageWithSuccessorsRequest
    {
        public ShiftPackageWithSuccessorsRequest(double daysDelta)
        {
            this.DaysDelta = daysDelta;
        }

        public double DaysDelta { get; set; }
    }
}
