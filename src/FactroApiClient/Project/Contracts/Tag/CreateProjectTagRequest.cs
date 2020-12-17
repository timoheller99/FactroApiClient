namespace FactroApiClient.Project.Contracts.Tag
{
    public class CreateProjectTagRequest
    {
        public CreateProjectTagRequest(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
