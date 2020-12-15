namespace FactroApiClient.Package.Contracts
{
    using System;

    public class CreatePackageRequest
    {
        public CreatePackageRequest(string title)
        {
            this.Title = title;
        }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public string Title { get; set; }

        public string OfficerId { get; set; }

        public string ParentPackageId { get; set; }
    }
}
