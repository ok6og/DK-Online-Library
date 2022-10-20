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
        private readonly KafkaProducer<int, Book> _kafkaProducer;
        private readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;
        private readonly KafkaConsumer<int,Book> _kafkaConsumer;
        public KafkaPersonController(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, KafkaProducer<int, Book> kafkaProducer, KafkaConsumer<int, Book> kafkaConsumer)
        {
            _kafkaSettings = kafkaSettings;
            _kafkaProducer = kafkaProducer;
            _kafkaConsumer = kafkaConsumer;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Produce))]
        public async Task Produce(int kay, Book person)
        {
            _kafkaProducer.Produce(kay, person);
            await Task.CompletedTask;
        }
        //[AllowAnonymous]
        //[HttpPost(nameof(Consume))]
        //public async  Task<IActionResult> Consume()
        //{
        //    return Ok(_kafkaConsumer.GetAll());
        //}
    }
}
