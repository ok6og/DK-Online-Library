using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK_Project.Models.Requests
{
    public class AddUpdateBookRequest
    {
        public int Id { get; set; }
        public string Title { get; init; }
        public int AuthorId { get; init; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Price { get; set; }
    }
}

