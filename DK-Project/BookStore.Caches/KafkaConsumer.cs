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
    public class KafkaConsumer<TKey, TValue>
    {
        private readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;
        private readonly MyKafkaSettings _thisKafkaSettings;
        private readonly ConsumerConfig _config;
        private readonly IConsumer<TKey, TValue> _consumer;
        public List<TValue> values;

        public KafkaConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _thisKafkaSettings = _kafkaSettings.CurrentValue.First(x => x.objectType.Contains(typeof(TValue).Name));
            _config = new ConsumerConfig()
            {
                BootstrapServers = _thisKafkaSettings.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _thisKafkaSettings.GroupId

            };
            _consumer = new ConsumerBuilder<TKey, TValue>(_config)
                .SetValueDeserializer(new MsgPackDeserializer<TValue>()).Build();
            _consumer.Subscribe(_thisKafkaSettings.Topic);
            values = new List<TValue>();
        }
        public List<TValue> GetAll()
        {
            return values;
        }
        public async void ConsumeValues()
        {
            var cr = _consumer.Consume();
            
            var value = cr.Message.Value;
            values.Add(value);
            Console.WriteLine($"CONSUMED: {value}");
        }
    }
}
