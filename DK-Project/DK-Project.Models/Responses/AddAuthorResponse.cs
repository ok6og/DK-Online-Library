using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace DK_Project.Models.Responses
{
    public class AddAuthorResponse : BaseResponse
    {
        public Author Author { get; set; }

    }
}
