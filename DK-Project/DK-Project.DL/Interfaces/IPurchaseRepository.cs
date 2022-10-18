using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace DK_Project.DL.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase?> SavePurchase(Purchase purchase);
        Task<Guid> DeletePurchase(Purchase purchase);
        Task<IEnumerable<Purchase>> GetPurchases(int userId);
    }
}
