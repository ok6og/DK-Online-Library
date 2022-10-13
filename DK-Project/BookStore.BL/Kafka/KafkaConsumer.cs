using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using DK_Project.Models.Configurations;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class KafkaConsumer<TKey,TValue> : IHostedService
    {
        private readonly IOptions<MyKafkaSettings> _kafkaSettings;
        private readonly ConsumerConfig _config;
        private readonly IConsumer<TKey, TValue> _consumer;
        List<TValue> values = new List<TValue>();

        public KafkaConsumer(IOptions<MyKafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _config = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _kafkaSettings.Value.GroupId
               
            };
            _consumer = new ConsumerBuilder<TKey, TValue>(_config)
                .SetValueDeserializer(new MsgPackDeserializer<TValue>()).Build();
            _consumer.Subscribe(_kafkaSettings.Value.Topic);
        }
        public List<TValue> Consume()
        {
            return values;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    var value = cr.Value;
                    values.Add(value);
                    Console.WriteLine($"CONSUMED: {value}");
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
