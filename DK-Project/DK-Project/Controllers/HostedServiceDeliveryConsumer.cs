using System.Threading.Tasks.Dataflow;
using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Services;
using BookStore.Caches;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Configurations;
using DK_Project.Models.Models;
using Microsoft.Extensions.Options;

namespace DK_Project.Controllers
{
    public class HostedServiceDeliveryConsumer : IHostedService
    {
        private readonly DeliveryConsumer _deliveryConsumer;
        private readonly PurchaseConsumer _purchaseConsumer;
        private readonly IPurchaseQuantityService _purchaseQuantityService;
        private readonly IOptionsMonitor<List<MyKafkaSettings>> _kafkaSettings;
        private readonly IBookRepository _bookRepository;
        private readonly IDeliveryQuantityService _quantityService;




        public HostedServiceDeliveryConsumer(IOptionsMonitor<List<MyKafkaSettings>> kafkaSettings, IBookRepository bookRepository, IDeliveryQuantityService quantityService, IPurchaseQuantityService purchaseQuantityService)
        {
            _kafkaSettings = kafkaSettings;
            _bookRepository = bookRepository;
            _quantityService = quantityService;
            _purchaseQuantityService = purchaseQuantityService;
            _deliveryConsumer = new DeliveryConsumer(_kafkaSettings, _bookRepository, _quantityService);
            _purchaseConsumer = new PurchaseConsumer(_kafkaSettings, _purchaseQuantityService, _bookRepository);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _deliveryConsumer.ConsumeValues(cancellationToken);
            _purchaseConsumer.ConsumeValues(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
