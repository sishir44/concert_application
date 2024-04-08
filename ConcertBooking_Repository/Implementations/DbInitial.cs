using ConcertBooking_Entities;
using ConcertBooking_Infrastructure;
using ConcertBooking_Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking_Repository.Implementations
{
    public class DbInitial : IDbInitial
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public DbInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
         
        public Task Seed()
        {
            if (!_roleManager.RoleExistsAsync(GlobalConfiguration.Admin_Role).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(GlobalConfiguration.Admin_Role)).GetAwaiter().GetResult();

                var user = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    //EmailConfirmed = true,
                };
                _userManager.CreateAsync(user, "Password@1").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, GlobalConfiguration.Admin_Role).GetAwaiter().GetResult();

            }
            return Task.CompletedTask;
        }
    }
}
