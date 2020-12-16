using Microsoft.AspNetCore.Identity;
using Diplom.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            string adminEmail = "admin1@gmail.com";
            string password = "admin1@gmail.comQWE";

            string customer1UserEmail = "1@mail.ru";
            string customer1Password = "1@mail.ruQWE";

            string customer2UserEmail = "2@mail.ru";
            string customer2Password = "2@mail.ruQWE";

            string manager1UserEmail = "manager1@mail.ru";
            string manager1Password = "manager1@mail.ruQWE";

            string manager2UserEmail = "manager2@mail.ru";
            string manager2Password = "manager2@mail.ruQWE";

            string driver1UserEmail = "driver1@mail.ru";
            string driver1Password = "driver1@mail.ruQWE";

            string driver2UserEmail = "driver2@mail.ru";
            string driver2Password = "driver2@mail.ruQWE";


            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await roleManager.FindByNameAsync("Driver") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Driver"));
            }

            if (await roleManager.FindByNameAsync("Manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            if (await roleManager.FindByNameAsync("Customer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                Users admin = new Users { Email = adminEmail, UserName = adminEmail };
                admin.EmailConfirmed = true;
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (await userManager.FindByNameAsync(customer1UserEmail) == null)
            {
                Users customer1User = new Users { Email = customer1UserEmail, UserName = customer1UserEmail };
                customer1User.EmailConfirmed = true;
                customer1User.PhoneNumber = "+375333123451";
                IdentityResult resultCustomer1User = await userManager.CreateAsync(customer1User, customer1Password);
                if (resultCustomer1User.Succeeded)
                {
                    context.Customers.Add(new Customers()
                    {
                        Id = customer1User.Id,
                        PasportData = "123456",
                        User = customer1User
                    });
                    context.SaveChanges();
                    await userManager.AddToRoleAsync(customer1User, "Customer");
                }
            }

            if (await userManager.FindByNameAsync(customer2UserEmail) == null)
            {
                Users customer2User = new Users { Email = customer2UserEmail, UserName = customer2UserEmail };
                customer2User.EmailConfirmed = true;
                customer2User.PhoneNumber = "+375333123452";
                IdentityResult resultCustomer2User = await userManager.CreateAsync(customer2User, customer2Password);
                if (resultCustomer2User.Succeeded)
                {
                    context.Customers.Add(new Customers()
                    {
                        Id = customer2User.Id,
                        PasportData = "1234569",
                        User = customer2User
                    });
                    context.SaveChanges();
                    await userManager.AddToRoleAsync(customer2User, "Customer");
                }
            }

            if (context.Organizations.FirstOrDefault(c => c.Id == 1 ) == null)
            {
                context.Organizations.Add(new Organizations
                {
                    //Id = 1,
                    Name = "Грузоперевозки1",
                    Address = "Минск",
                    PhoneNamber = "+37533123456",
                    Email = "Грузоперевозки1@mail.ru"
                });
                context.SaveChanges();

                context.Cars.Add(new Cars
                {
                   // Id = 1,
                    StateNamber = "3333",
                    CarBrand = "Маз",
                    CarringCapacity = 20,
                    BodyVolume = 120,
                    OrganizationId = 1
                });

                context.Cars.Add(new Cars
                {
                    //Id = 2,
                    StateNamber = "4444",
                    CarBrand = "Маз",
                    CarringCapacity = 15,
                    BodyVolume = 100,
                    OrganizationId = 1
                });

                context.SaveChanges();
            }

            if (await userManager.FindByNameAsync(manager1UserEmail) == null)
            {
                Users manager1User = new Users { Email = manager1UserEmail, UserName = manager1UserEmail };
                manager1User.EmailConfirmed = true;
                IdentityResult resultManager1User = await userManager.CreateAsync(manager1User, manager1Password);
                if (resultManager1User.Succeeded)
                {
                    context.Employees.Add(new Employees()
                    {
                        Id = manager1User.Id,
                        EmploymentDate = DateTime.Now,
                        PasportData = "111111",
                        DateOfBirth = DateTime.Now,
                        Salary = 1000,
                        Address = "Минск",
                        User = manager1User,
                        Manager = new Managers() { Id = manager1User.Id, Education = "Высшее" },
                        OrganizationId = 1
                    });

                    context.SaveChanges();
                    await userManager.AddToRoleAsync(manager1User, "Manager");
                }

            }

            if (await userManager.FindByNameAsync(manager2UserEmail) == null)
            {
                Users manager2User = new Users { Email = manager2UserEmail, UserName = manager2UserEmail };
                manager2User.EmailConfirmed = true;
                IdentityResult resultManager2User = await userManager.CreateAsync(manager2User, manager2Password);
                if (resultManager2User.Succeeded)
                {
                    context.Employees.Add(new Employees()
                    {
                        Id = manager2User.Id,
                        EmploymentDate = DateTime.Now,
                        PasportData = "111112",
                        DateOfBirth = DateTime.Now,
                        Salary = 2000,
                        Address = "Могилёв",
                        User = manager2User,
                        Manager = new Managers() { Id = manager2User.Id, Education = "Высшее" },
                        OrganizationId = 1
                    });

                    context.SaveChanges();
                    await userManager.AddToRoleAsync(manager2User, "Manager");
                }
            }

            if (await userManager.FindByNameAsync(driver1UserEmail) == null)
            {
                Users driver1User = new Users { Email = driver1UserEmail, UserName = driver1UserEmail };
                driver1User.EmailConfirmed = true;
                IdentityResult resultDriver1User = await userManager.CreateAsync(driver1User, driver1Password);
                if (resultDriver1User.Succeeded)
                {
                    context.Employees.Add(new Employees()
                    {
                        Id = driver1User.Id,
                        EmploymentDate = DateTime.Now,
                        PasportData = "111113",
                        DateOfBirth = DateTime.Now,
                        Salary = 5000,
                        Address = "Могилёв",
                        User = driver1User,
                        Driver = new Drivers()
                        {
                            Id = driver1User.Id,
                            DrivingLicense = "12345678",
                            DrivingExperience = 10
                        },
                        OrganizationId = 1
                    });

                    context.SaveChanges();
                    await userManager.AddToRoleAsync(driver1User, "Driver");
                }
            }

            if (await userManager.FindByNameAsync(driver2UserEmail) == null)
            {
                Users driver2User = new Users { Email = driver2UserEmail, UserName = driver2UserEmail };
                driver2User.EmailConfirmed = true;
                IdentityResult resultDriver2User = await userManager.CreateAsync(driver2User, driver2Password);
                if (resultDriver2User.Succeeded)
                {
                    context.Employees.Add(new Employees()
                    {
                        Id = driver2User.Id,
                        EmploymentDate = DateTime.Now,
                        PasportData = "111114",
                        DateOfBirth = DateTime.Now,
                        Salary = 5500,
                        Address = "Могилёв",
                        User = driver2User,
                        Driver = new Drivers()
                        {
                            Id = driver2User.Id,
                            DrivingLicense = "87654321",
                            DrivingExperience = 5
                        },
                        OrganizationId = 1
                    });

                    context.SaveChanges();
                    await userManager.AddToRoleAsync(driver2User, "Driver");
                }
            }
        }
    }
}

