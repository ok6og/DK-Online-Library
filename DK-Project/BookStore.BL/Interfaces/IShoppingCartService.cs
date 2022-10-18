using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<IEnumerable<ShoppingCart>> GetContent(int userId);
        public Task<ShoppingCart> AddToCart(int userId,int bookId);
        public Task<Book> RemoveFromCart(int userId,int bookId);
        public Task EmptyCart(int userId);
        public Task FinishPurchase(int userId);
    }
}
