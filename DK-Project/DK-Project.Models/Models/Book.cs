using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK_Project.Models.Models
{
    public class Book
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public int AuthorId { get; init; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Price { get; set; }
    }
}
