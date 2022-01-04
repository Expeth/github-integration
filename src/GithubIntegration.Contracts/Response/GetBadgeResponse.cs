namespace GithubIntegration.Contracts.Response
{
    public sealed class GetBadgeResponse
    {
        public string SchemaVersion { get; set; }
        public string Label { get; set; }
        public string Message { get; set; }
        public string Color { get; set; }

        public GetBadgeResponse(string schemaVersion, string label, string message, string color)
        {
            SchemaVersion = schemaVersion;
            Label = label;
            Message = message;
            Color = color;
        }
    }
}