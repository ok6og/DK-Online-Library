using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using DK_Project.Models.Configurations;
using DK_Project.Models.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class KafkaProducer<TKey,TValue>
    {
        private readonly IOptions<MyKafkaSettings> _kafkaSettings;
        private readonly ProducerConfig _config;
        private readonly IProducer<TKey, TValue> _producer;

        public KafkaProducer(IOptions<MyKafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _config = new ProducerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers
            };
            _producer = new ProducerBuilder<TKey, TValue>(_config)
                .SetValueSerializer(new MsgPackSerializer<TValue>()).Build();
        }

        public async void Produce(TKey key, TValue value)
        {
            try
            {
                var msg = new Message<TKey, TValue>()
                {
                    Key = key,
                    Value = value
                };
                var result = await _producer.ProduceAsync(_kafkaSettings.Value.Topic, msg);
                if (result != null)
                    Console.WriteLine($"Delivered: {result.Value} to {result.TopicPartitionOffset}");
            }
            catch (ProduceException<TKey, TValue>ex)
            {
                Console.WriteLine($"Delivery failed: {ex.Error.Reason}"); ;
            }
        }
    }
}
