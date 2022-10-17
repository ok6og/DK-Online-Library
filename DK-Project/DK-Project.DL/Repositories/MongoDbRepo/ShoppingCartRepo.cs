using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DK_Project.DL.Repositories.MongoDbRepo
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private readonly IOptions<MongoDbModel> _mongoDbOptions;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ShoppingCart> _collection;
        private readonly IBookRepository _bookRepository;
        private readonly IPurchaseRepository _purchaseRepo;



        public ShoppingCartRepo(IOptions<MongoDbModel> mongoDbOptions, IBookRepository bookRepository, IPurchaseRepository purchaseRepo)
        {
            _mongoDbOptions = mongoDbOptions;
            MongoClient dbClient = new MongoClient(_mongoDbOptions.Value.ConnectionString);
            _database = dbClient.GetDatabase(_mongoDbOptions.Value.DatabaseName);
            _collection = _database.GetCollection<ShoppingCart>(_mongoDbOptions.Value.ShoppingCartCollectionName);
            _bookRepository = bookRepository;
            _purchaseRepo = purchaseRepo;
        }

        public async Task<ShoppingCart?> AddCart(ShoppingCart purchase)
        {
            await _collection.InsertOneAsync(purchase);
            return purchase;
        }
        //public async Task<ShoppingCart?> AddToCart(int bookId,int userId)
        //{
        //    var book = await _bookRepository.GetById(bookId);
        //    var cart = await GetShoppingCart(userId);
        //    if (book == null)
        //    {
        //        return new ShoppingCart();
        //    }
        //    _collection.UpdateOneAsync()
        //    cart.Books.Add(book);
        //    return cart;
        //}

        public async Task EmptyCart(int userId)
        {
            await _collection.DeleteManyAsync(x => x.UserId == userId);
        }

        public async Task FinishPurchase(int userId)
        {
            var finishedShippingCart = await GetShoppingCart(userId);
            if (finishedShippingCart == null)
            {
                return;
            }
            var purchase = new Purchase()
            {
                Id = Guid.NewGuid(),
                Books = finishedShippingCart.Books,
                TotalMoney = finishedShippingCart.Books.Sum(x=> x.Price),
                UserId = userId,
            };
            await _purchaseRepo.SavePurchase(purchase);
        }

        public async Task<IEnumerable<ShoppingCart>> GetContent(int userId)
        {
            var collection = await _collection.FindAsync(x => x.UserId == userId);
            return await collection.ToListAsync();
        }

        public async Task<ShoppingCart> GetShoppingCart(int userId)
        {
            var collection = await _collection.FindAsync(x => x.UserId == userId);
            return collection.FirstOrDefault();
        }


        public async Task<Book> RemoveFromCart(int userId, int bookId)
        {
            var shoppingCart = await GetShoppingCart(userId);
            var bookToRemove = shoppingCart.Books.FirstOrDefault(x=>x.Id == bookId);
            shoppingCart.Books.Remove(bookToRemove);
            return bookToRemove;
        }

        public async Task RemoveCart(int userId)
        {
            await _collection.DeleteOneAsync(x => x.UserId == userId);
        }
    }
}
