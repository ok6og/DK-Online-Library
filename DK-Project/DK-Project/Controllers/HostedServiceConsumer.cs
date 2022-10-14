using BookStore.BL.Kafka;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;

namespace DK_Project.Controllers
{
    public class HostedServiceConsumer<TKey,TValue> : IHostedService
    {
        private readonly KafkaConsumer<TKey, TValue> _kafkaConsumer;

        public HostedServiceConsumer(KafkaConsumer<TKey, TValue> kafkaConsumer)
        {
            _kafkaConsumer = kafkaConsumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _kafkaConsumer.ConsumeValues();
                }
            },cancellationToken);
            return Task.CompletedTask;


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
