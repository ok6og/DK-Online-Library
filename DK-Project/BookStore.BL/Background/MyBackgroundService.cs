using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Background
{
    public class MyBackgroundService : IHostedService
    {
        private readonly ILogger<MyBackgroundService> _logger;

        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"This is {nameof(StartAsync)}");
            Task.Run(async ()=>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Hello" + DateTime.Now);
                    await Task.Delay(1000);
                }
            });
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"This is {nameof(StopAsync)}");
        }

        //protected override Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    _logger.LogInformation($"Hello from {nameof(MyBackgroundService)}");
        //    return Task.CompletedTask;
        //}
    }
}
