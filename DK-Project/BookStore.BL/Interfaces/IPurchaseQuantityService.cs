using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPurchaseQuantityService
    {
        public Task<Book> CheckQuantityDelivery(Purchase purchase);
    }
}
