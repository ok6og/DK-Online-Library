using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace DK_Project.DL.Interfaces
{
    public interface IShoppingCartRepo
    {
        public Task<ShoppingCart?> AddCart(ShoppingCart purchase);
        public Task EmptyCart(int userId);
        public Task FinishPurchase(int userId);
        public Task<IEnumerable<ShoppingCart>> GetContent(int userId);
        public Task<Book?> RemoveFromCart(int userId, int bookId);
        public Task<ShoppingCart> GetShoppingCart(int userId);
        public Task RemoveCart(int userId);
    }
}
