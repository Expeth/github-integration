using System;

namespace GithubIntegration.Host.Configs
{
    public class HttpConfig
    {
        public string Uri { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}