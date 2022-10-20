using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class PurchaseConsumer : KafkaConsumer<Guid,Purchase>
    {
        private readonly IPurchaseQuantityService _purchaseQuantityService;
        private readonly IBookRepository _bookRepository;
        private readonly TransformBlock<Purchase, Book> _transformBlock;
        private readonly ActionBlock<Book> _actionBlock;
        public PurchaseConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, IPurchaseQuantityService purchaseQuantityService, IBookRepository bookRepository) : base(kafkaSettings)
        {
            _purchaseQuantityService = purchaseQuantityService;
            _bookRepository = bookRepository;
            _transformBlock = new TransformBlock<Purchase, Book>(purchase =>
            {
                var book = _purchaseQuantityService.CheckQuantityDelivery(purchase);
                return book;
            });
            _actionBlock = new ActionBlock<Book>(value =>
            {
                _bookRepository.UpdateBook(value);
                Console.WriteLine($"{value.Id} - {value.Quantity} Purchase");
            });
            _transformBlock.LinkTo(_actionBlock);
        }

        public override Task HandleMesseges(Purchase purchase)
        {
            _transformBlock.Post(purchase);
            return Task.CompletedTask;
        }
    }
}
