using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK_Project.Models.Models
{
    public interface IKey<out T>
    {
        T GetKey();
    }
}
