using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoushGah.Common.Wrappers;
using NoushGah.DataAccess.IdentityModel;
using NoushGah.Model.Entities;
using NoushGah.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Repository
{
    public class SignInRepository : ISignInRepository
    {
        private readonly SignInManager<SystemUser> signInManager;
        private readonly UserManager<SystemUser> userManager;
        private readonly IBasketRepository basketRepository;
        private readonly IUserRepository userRepository;

        public SignInRepository(
            SignInManager<SystemUser> signInManager, 
            UserManager<SystemUser> userManager,
            IBasketRepository basketRepository,
            IUserRepository userRepository)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.basketRepository = basketRepository;
            this.userRepository = userRepository;
        }

        public async Task Login(string phoneNumber)
        {
            var user = await userRepository.GetUserByPhoneNumber(phoneNumber);

            if (user == null) throw new NullReferenceException();

            var sysUsr = new SystemUser
            {
                Id = user.UserId,
                PhoneNumber = phoneNumber,
                UserName = phoneNumber,
            };

            await signInManager.SignInAsync(sysUsr, true);
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task SignUp(string phoneNumber)
        {
            var newUser = await userRepository.CreateUser(phoneNumber);

            var basketWrapper = new BasketWrapper()
            {
                UserId = newUser.UserId,
                BasketStatus = Model.Enums.BasketStatusEnum.Reset,
            };
            var newBasket = await basketRepository.CreateBasket(basketWrapper);
        }
    }
}
