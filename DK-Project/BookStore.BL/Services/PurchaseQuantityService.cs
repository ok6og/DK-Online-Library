using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;

namespace BookStore.BL.Services
{
    public class PurchaseQuantityService : IPurchaseQuantityService
    {
        private readonly IBookRepository _bookRepository;

        public PurchaseQuantityService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Book> CheckQuantityDelivery(Purchase purchase)
        {
            if (purchase == null)
            {
                return null;
            }
            var book = await _bookRepository.GetById(purchase.Books.FirstOrDefault().Id);
            if (book == null)
            {
                return null;
            }

            if (book.Quantity < purchase.Books.Count())
            {
                book.Quantity = 0;
            }
            else
            {
                book.Quantity = book.Quantity - purchase.Books.Count();
            }
            return book;
        }
    }
}
