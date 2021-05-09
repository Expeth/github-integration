namespace GithubIntegration.Contracts.Model
{
    public sealed class RepositoryInfo
    {
        /// <summary>
        /// Setters and parameterless constructor are needed for serialization/deserialization
        /// </summary>
        public string Name { get; set; }
        public string FullName { get; set; }
        public string HtmlUrl { get; set; }
        public string CloneUrl { get; set; }
        public string Description { get; set; }

        public RepositoryInfo() { }
        
        public RepositoryInfo(string name, string fullName, string htmlUrl, string cloneUrl, string description)
        {
            Name = name;
            FullName = fullName;
            HtmlUrl = htmlUrl;
            CloneUrl = cloneUrl;
            Description = description;
        }
    }
}