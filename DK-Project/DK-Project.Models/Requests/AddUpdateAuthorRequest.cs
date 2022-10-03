using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK_Project.Models.Requests
{
    public class AddUpdateAuthorRequest
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
        public DateTime DateOfBirth { get; set; }
        public string Nickname { get; init; }

    }
}
