using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models.Users;

namespace DK_Project.DL.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserInfo?> GetUserInfo(string userName, string password);
    }
}
