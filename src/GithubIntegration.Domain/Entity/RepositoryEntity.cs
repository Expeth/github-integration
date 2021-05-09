namespace GithubIntegration.Domain.Entity
{
    public sealed class RepositoryEntity
    {
        public string Name { get; }
        public string FullName { get; }
        public string Description { get; }
        public string HtmlUrl { get; }
        public string CloneUrl { get; }

        public RepositoryEntity(string name, string fullName, string description, string htmlUrl, string cloneUrl)
        {
            Name = name;
            FullName = fullName;
            Description = description;
            HtmlUrl = htmlUrl;
            CloneUrl = cloneUrl;
        }
    }
}