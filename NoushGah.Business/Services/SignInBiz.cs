using NoushGah.Business.Interfaces;
using NoushGah.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Business.Services
{
    public class SignInBiz : ISignInBiz
    {
        private readonly ISignInRepository signInRepository;

        public SignInBiz(ISignInRepository signInRepository)
        {
            this.signInRepository = signInRepository;
        }

        public string GetVerifyCode(string phoneNumber)
        {
            var code = new Random().Next(1001,9998);
            return code.ToString();
        }

        public async Task Login(string phoneNumber)
        {
            await signInRepository.Login(phoneNumber);
        }

        public async Task Logout()
        {
            await signInRepository.Logout();
        }

        public async Task SignUp(string phoneNumber)
        {
            await signInRepository.SignUp(phoneNumber);
        }
    }
}
