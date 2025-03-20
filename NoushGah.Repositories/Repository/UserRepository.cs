using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoushGah.Common.Wrappers;
using NoushGah.DataAccess.IdentityModel;
using NoushGah.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<SystemUser> userManager;

        public UserRepository(UserManager<SystemUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserWrapper> CreateUser(string phoneNumber)
        {
            var newUser = new SystemUser
            {
                PhoneNumber = phoneNumber,
                UserName = phoneNumber,
            };

            var result = await userManager.CreateAsync(newUser);

            if (!result.Succeeded)
            {
                var errorMessages = new StringBuilder();

                foreach (var item in result.Errors)
                {
                    errorMessages.AppendLine(item.Description);
                }

                throw new Exception(errorMessages.ToString());
            }

            return new UserWrapper()
            {
                PhoneNumber = newUser.PhoneNumber,
                UserId = newUser.Id,
            };
        }

        public async Task<UserWrapper> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await userManager.Users.Where(u => u.PhoneNumber == phoneNumber).Select(u => new UserWrapper()
            {
                PhoneNumber = u.PhoneNumber,
                UserId = u.Id,
            }).SingleOrDefaultAsync();

            if (user == null) throw new NullReferenceException();

            return user;
        }
    }
}
