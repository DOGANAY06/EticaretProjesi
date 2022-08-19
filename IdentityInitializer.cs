using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EticaretProjesi.Entities;
using Microsoft.AspNetCore.Identity;

namespace EticaretProjesi
{
    public class IdentityInitializer
    {//kullanıcı oluşturma giriş için 
        public static void OlusturAdmin(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            AppUser appUser = new AppUser
            {
                Name = "Dogan",
                SurName = "Ay",
                UserName = "Dogan06"
            };
            if (userManager.FindByNameAsync("Dogan06").Result==null)
            {//böyle bir kullanıcı yoksa oluşturalım
                var identityResult = userManager.CreateAsync(appUser,"1").Result;
            }

            if (roleManager.FindByNameAsync("Admin").Result==null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                var identityResult = roleManager.CreateAsync(role).Result;

                var result = userManager.AddToRoleAsync(appUser, role.Name).Result;
            }
        }

       
    }
}
