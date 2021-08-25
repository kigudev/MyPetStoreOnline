using ASPNETIdentity.Data;
using Microsoft.AspNetCore.Identity;
using MyPetStoreOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Data
{
    public class ApplicationContextSeed
    {
        public static async Task SeedIdentityAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // definir los roles que queremos que existan en el sistema
            var roles = Enum.GetNames<Role>();
            // si los roles (o alguno de ellos) no existen
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }

            // definir usuario que queramos que sea el administrador
            var adminUserName = "admin@gmail.com";
            var adminEmail = "admin@gmail.com";
            var adminPassword = "qweqwe";
            // si el usuario no existe
            var user = await userManager.FindByNameAsync(adminUserName);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    FirstName = "Pedro",
                    LastName = "Admin"
                };
                var result = await userManager.CreateAsync(user, adminPassword);
                if(result.Succeeded)
                    await userManager.AddToRoleAsync(user, Role.Administrator.ToString());
            }

            var managerUserName = "manager@gmail.com";
            var manager = await userManager.FindByNameAsync(managerUserName);

            if (manager == null)
            {
                manager = new ApplicationUser
                {
                    UserName = managerUserName,
                    Email = managerUserName,
                    FirstName = "Mayra",
                    LastName = "Manager"
                };
                var result = await userManager.CreateAsync(manager, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(manager, Role.Manager.ToString());
            }
        }

        public static async Task SeedAsync(ApplicationContext context)
        {
            if (!context.ProductBrands.Any())
            {
                await context.ProductBrands.AddRangeAsync(
                    new List<ProductBrand>
                    {
                        new ProductBrand{ Name = "My Doggy"},
                        new ProductBrand{ Name = "Purina"},
                        new ProductBrand{ Name = "Kirkland"},
                        new ProductBrand{ Name = "DogChow"},
                        new ProductBrand{ Name = "CatChow"},
                        new ProductBrand{ Name = "Pedigree"},
                        new ProductBrand{ Name = "Whiskas"},
                    }
                );

                await context.SaveChangesAsync();
            }

            if (!context.ProductTypes.Any())
            {
                await context.ProductTypes.AddRangeAsync(
                    new List<ProductType>
                    {
                        new ProductType{ Name = "Food"},
                        new ProductType{ Name = "Accesories"},
                        new ProductType{ Name = "Clothes"},
                        new ProductType{ Name = "Cleaning"},
                        new ProductType{ Name = "Furniture"}
                    }
                );

                await context.SaveChangesAsync();
            }

        }
    }
}