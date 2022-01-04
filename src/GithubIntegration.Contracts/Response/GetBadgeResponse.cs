namespace GithubIntegration.Contracts.Response
{
    public sealed class GetBadgeResponse
    {
        public int SchemaVersion { get; set; }
        public string Label { get; set; }
        public string Message { get; set; }
        public string Color { get; set; }

        public GetBadgeResponse(int schemaVersion, string label, string message, string color)
        {
            SchemaVersion = schemaVersion;
            Label = label;
            Message = message;
            Color = color;
        }
    }
}