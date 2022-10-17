using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DK_Project.DL.Repositories.MongoDbRepo
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly  IOptions<MongoDbModel> _mongoDbOptions;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Purchase> _collection;

        public PurchaseRepository(IOptions<MongoDbModel> mongoDbOptions)
        {
            _mongoDbOptions = mongoDbOptions;
            MongoClient dbClient = new MongoClient(_mongoDbOptions.Value.ConnectionString);
            _database = dbClient.GetDatabase(_mongoDbOptions.Value.DatabaseName);
            _collection = _database.GetCollection<Purchase>(_mongoDbOptions.Value.CollectionName);
        }
        public async Task<Guid> DeletePurchase(Purchase purchase)
        {
            await _collection.DeleteOneAsync(x => x.Id == purchase.Id);
            return purchase.Id;
        }
        public async Task<IEnumerable<Purchase>> GetPurchases(int userId)
        {
            var collection = await _collection.FindAsync(x => x.UserId == userId);
            return await collection.ToListAsync();
        }
        public async Task<Purchase?> SavePurchase(Purchase purchase)
        {
            await _collection.InsertOneAsync(purchase);
            return purchase;
        }
    }
}
