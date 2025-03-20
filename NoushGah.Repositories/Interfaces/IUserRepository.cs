using NoushGah.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserWrapper> CreateUser(string phoneNumber);
        Task<UserWrapper> GetUserByPhoneNumber(string phoneNumber);

    }
}
