using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Configurations;
using DK_Project.Models.Models;
using Microsoft.Extensions.Options;

namespace BookStore.Caches
{
    public class DeliveryConsumer : KafkaConsumer<int, Delivery>
    {
        private readonly IBookRepository _bookRepository;
        private readonly TransformBlock<Delivery, Book> _transformBlock;
        private readonly ActionBlock<Book> _actionBlock;
        private readonly IDeliveryQuantityService _quantityService;

        public DeliveryConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, IBookRepository bookRepository, IDeliveryQuantityService quantityService) : base(kafkaSettings)
        {
            _bookRepository = bookRepository;
            _quantityService = quantityService;
            _transformBlock = new TransformBlock<Delivery, Book>(delivery =>
            {
                var book = _quantityService.CheckQuantityDelivery(delivery);
                return book;
            });
            _actionBlock = new ActionBlock<Book>(value =>
            {
                Console.WriteLine($"{value.Id} - {value.Quantity}");
                _bookRepository.UpdateBook(value);
            });
            _transformBlock.LinkTo(_actionBlock);
        }
        public override Task HandleMesseges(Delivery delivery)
        {
            _transformBlock.Post(delivery);
            return Task.CompletedTask;
        }
    }
}
