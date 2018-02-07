using CodeX.Core.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeX.Core.API
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //This example just creates an Administrator role and one Admin users
        public async void Initialize()
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //create database schema if none exists
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                //If there is already an Administrator role, abort
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();


                // In Startup iam creating first Admin Role and creating a default Admin User  
                if (!(await roleManager.RoleExistsAsync("Admin")))
                {

                    // first we create Admin rool 
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    await roleManager.CreateAsync(new ApplicationRole("Admin"));

                    //Here we create a Admin super user who will maintain the website                 

                    var user = new ApplicationUser();
                    user.UserName = "ck.nitin@outlook.com";
                    user.Email = "ck.nitin@outlook.com";
                    string userPassword = "Password@123";



                    //Add default User to Role Admin 
                    var success = await userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        EmailConfirmed = true
                    }, userPassword);

                    if (success.Succeeded)
                    {
                        await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), "Admin");
                    }
                }

                // creating Creating Manager role  
                if (!(await roleManager.RoleExistsAsync("Manager")))
                {
                    await roleManager.CreateAsync(new ApplicationRole("Manager"));
                }

                // creating Creating Employee role  
                if (!(await roleManager.RoleExistsAsync("Employee")))
                {
                    await roleManager.CreateAsync(new ApplicationRole("Employee"));
                }

                // creating Creating Employee role  
                if (!(await roleManager.RoleExistsAsync("Free User")))
                {
                    await roleManager.CreateAsync(new ApplicationRole("Free User"));
                }

                // creating Creating Basic role  
                if (!(await roleManager.RoleExistsAsync("Basic")))
                {
                    await roleManager.CreateAsync(new ApplicationRole("Basic"));
                }

                // creating Creating Gold role  
                if (!(await roleManager.RoleExistsAsync("Gold")))
                {
                    await roleManager.CreateAsync(new ApplicationRole("Gold"));
                }

                // creating Creating Premium role  
                if (!(await roleManager.RoleExistsAsync("Premium")))
                {
                    await roleManager.CreateAsync(new ApplicationRole("Premium"));
                }
            }
        }
    }
}



