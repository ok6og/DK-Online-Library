using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DK_Project.Models.Models
{
    public record Purchase
    {
        public Guid Id { get; set; }
        public IList<Book> Books { get; set; } = new List<Book>();
        public decimal TotalMoney { get; set; }
        public int UserId { get; set; }
    }
}
