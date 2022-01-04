namespace GithubIntegration.Domain.Entity
{
    public sealed class ReleaseEntity
    {
        public string Name { get; }
        public string TagName { get; }
        public bool IsPrerelease { get; }

        public ReleaseEntity(string name, string tagName, bool isPrerelease)
        {
            Name = name;
            TagName = tagName;
            IsPrerelease = isPrerelease;
        }
    }
}