using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK_Project.Models.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public IList<Book> Books { get; set; }=new List<Book>();
    }
}
