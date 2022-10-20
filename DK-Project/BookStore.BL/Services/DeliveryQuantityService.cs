using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;

namespace BookStore.BL.Services
{
    public class DeliveryQuantityService : IDeliveryQuantityService
    {
        private readonly IBookRepository _bookRepository;

        public DeliveryQuantityService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CheckQuantityDelivery(Delivery delivery)
        {
            if (delivery == null)
            {
                return null;
            }
            var book =await _bookRepository.GetById(delivery.Book.Id);
            if (book == null)
            {
                return null;
            }
            book.Quantity = book.Quantity + delivery.Quantity;
            return book;
        }
    }
}
