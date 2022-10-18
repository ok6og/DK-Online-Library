using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPurchaseService
    {
        public Task<Guid> DeletePurchase(Purchase purchase);
        public Task<IEnumerable<Purchase>> GetPurchases(int userId);
        public Task<Purchase?> SavePurchase(Purchase purchase);
    }
}
