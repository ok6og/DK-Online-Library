using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace DK_Project.Models.Models
{
    [MessagePackObject]
    public class Book : IKey<int>
    {
        [Key(0)]
        public int Id { get; init; }
        [Key(1)]

        public string Title { get; set; }
        [Key(2)]

        public int AuthorId { get; init; }
        [Key(3)]

        public int Quantity { get; set; }
        [Key(4)]

        public DateTime LastUpdated { get; set; }
        [Key(5)]

        public decimal Price { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
