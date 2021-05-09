using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GithubIntegration.Domain.Abstraction;
using GithubIntegration.Domain.Abstraction.Agent;
using GithubIntegration.Domain.Entity;
using GithubIntegration.Host.Configs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

namespace GithubIntegration.Host.Services.Background
{
    public class PullUpdatesHostedService : BackgroundService
    {
        private static readonly ILogger Logger = Log.ForContext<PullUpdatesHostedService>();

        private readonly IGithubAgent _githubAgent;
        private readonly IInMemoryCache<IEnumerable<RepositoryEntity>?> _repositoriesCache;
        private readonly IOptionsMonitor<PullUpdatesConfig> _pullUpdatesCfg;

        public PullUpdatesHostedService(IInMemoryCache<IEnumerable<RepositoryEntity>?> repositoriesCache,
            IOptionsMonitor<PullUpdatesConfig> pullUpdatesCfg, IGithubAgent githubAgent)
        {
            _repositoriesCache = repositoriesCache;
            _pullUpdatesCfg = pullUpdatesCfg;
            _githubAgent = githubAgent;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
            Task.Factory.StartNew(async () => await ExecuteAsyncInternal(stoppingToken), stoppingToken,
                TaskCreationOptions.LongRunning, TaskScheduler.Default);

        private async Task ExecuteAsyncInternal(CancellationToken stoppingToken)
        {
            while (true)
            {
                Logger.Debug("Starting to pull updates");
                var cfg = _pullUpdatesCfg.CurrentValue;

                try
                {
                    var repositoriesResult = await _githubAgent.GetRepositories(cfg.Username);
                    _repositoriesCache.SetItem(cfg.Username, repositoriesResult,
                        cfg.Interval.Add(TimeSpan.FromHours(1)));
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Exception occured while updating repositories list");
                }

                await Task.Delay(cfg.Interval, stoppingToken);
            }
        }
    }
}