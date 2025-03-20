using NoushGah.Business.Interfaces;
using NoushGah.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Business.Services
{
    public class UserBiz : IUserBiz
    {
        public Task<UserWrapper> CreateUser(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<UserWrapper> GetUserByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
