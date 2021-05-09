using System;

namespace GithubIntegration.Host.Configs
{
    public class PullUpdatesConfig
    {
        public TimeSpan Interval { get; set; }
        public string Username { get; set; }
    }
}