using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Interfaces
{
    public interface ISignInRepository
    {
        Task Login(string phoneNumber);
        Task Logout();
        Task SignUp(string phoneNumber);
    }
}
