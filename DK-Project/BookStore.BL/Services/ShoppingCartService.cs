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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IShoppingCartRepo _shoppingCartRepo;

        public ShoppingCartService(IBookRepository bookRepository, IShoppingCartRepo shoppingCartRepo)
        {
            _bookRepository = bookRepository;
            _shoppingCartRepo = shoppingCartRepo;
        }
        public async Task<ShoppingCart?> AddToCart(int userId, int bookId)
        {
            var book =await _bookRepository.GetById(bookId);
            var cart =await _shoppingCartRepo.GetShoppingCart(userId);

            if (book == null)
            {
                return null;
            }
            if (cart == null)
            {
                ShoppingCart purchase1 = new ShoppingCart()
                {
                    UserId = userId,
                    Id = Guid.NewGuid(),
                    Books = new List<Book>() { book}
                };
                _shoppingCartRepo.AddCart(purchase1);
                return purchase1;
            }
            _shoppingCartRepo.RemoveCart(userId);
            var books = cart.Books;
            books.Add(book);
            ShoppingCart purchase = new ShoppingCart()
            {
                UserId = userId,
                Id = cart.Id,
                Books = books
            };
            _shoppingCartRepo.AddCart(purchase);
            return purchase;
        }
        public async Task EmptyCart(int userId)
        {
            await _shoppingCartRepo.EmptyCart(userId);
        }
        public async Task FinishPurchase(int userId)
        {
            await _shoppingCartRepo.FinishPurchase(userId);
            await _shoppingCartRepo.RemoveCart(userId);
        }
        public async Task<IEnumerable<ShoppingCart>> GetContent(int userId)
        {
            var content = await _shoppingCartRepo.GetContent(userId);
            return content;
        }
        public async Task<Book?> RemoveFromCart(int userId, int bookId)
        {
            var book = await _shoppingCartRepo.RemoveFromCart(userId, bookId);
            return book;
        }
    }
}
