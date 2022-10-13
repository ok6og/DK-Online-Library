using BookStore.BL.Kafka;
using DK_Project.Models.Configurations;
using DK_Project.Models.Models;
using DK_Project.Models.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DK_Project.Controllers
{
    public class KafkaPersonController : ControllerBase
    {
        private readonly KafkaProducer<int, string> _kafkaProducer;
        private readonly IOptions<MyKafkaSettings> _kafkaSettings;
        private readonly KafkaConsumer<int,string> _kafkaConsumer;


        public KafkaPersonController(IOptions<MyKafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _kafkaProducer = new KafkaProducer<int, string>(_kafkaSettings);
            _kafkaConsumer = new KafkaConsumer<int, string>(_kafkaSettings);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Produce))]
        public async Task Produce(int kay, string person)
        {
            _kafkaProducer.Produce(kay, person);
            await Task.CompletedTask;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Consume))]
        public async  Task<IActionResult> Consume()
        {
            return Ok(_kafkaConsumer.Consume());
        }
    }
}
