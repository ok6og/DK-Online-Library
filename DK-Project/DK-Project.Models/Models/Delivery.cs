using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace DK_Project.Models.Models
{
    [MessagePackObject]
    public class Delivery
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public Book Book { get; set; }
        [Key(2)]
        public int Quantity { get; set; }
    }
}
