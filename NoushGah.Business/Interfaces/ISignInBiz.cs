using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Business.Interfaces
{
    public interface ISignInBiz
    {
        Task Login(string phoneNumber);
        Task Logout();
        Task SignUp(string phoneNumber);
        string GetVerifyCode(string phoneNumber);
    }
}
