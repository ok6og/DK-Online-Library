using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.MongoDbRepo;
using DK_Project.Models.Models;

namespace BookStore.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public Task<Guid> DeletePurchase(Purchase purchase)
        {
            return _purchaseRepository.DeletePurchase(purchase);
        }

        public Task<IEnumerable<Purchase>> GetPurchases(int userId)
        {
            return _purchaseRepository.GetPurchases(userId);
        }

        public Task<Purchase?> SavePurchase(Purchase purchase)
        {
            return _purchaseRepository.SavePurchase(purchase);
        }
    }
}
