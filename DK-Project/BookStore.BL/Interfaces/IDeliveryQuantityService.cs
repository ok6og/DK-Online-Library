using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Kafka;
using DK_Project.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IDeliveryQuantityService
    {
        public Task<Book> CheckQuantityDelivery(Delivery delivery);
    }
}
