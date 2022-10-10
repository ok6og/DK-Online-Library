using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BookStore.BL.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(UserInfo user);
        Task<UserInfo?> CheckUserAndPassword(string userName, string password);
        public Task<IEnumerable<string>> GetUserRoles(UserInfo user);
    }
}
