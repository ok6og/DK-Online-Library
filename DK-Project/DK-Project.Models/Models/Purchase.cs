using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DK_Project.Models.Models
{
    [MessagePackObject]
    public record Purchase
    {
        [Key(0)]
        public Guid Id { get; set; }
        [Key(1)]
        public IList<Book> Books { get; set; } = new List<Book>();
        [Key(2)]
        public decimal TotalMoney { get; set; }
        [Key(3)]
        public int UserId { get; set; }
        [Key(4)]
        public IEnumerable<string> AdditionalInfo { get; set; } = Enumerable.Empty<string>();
    }
}
