using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Data;
using Diplom.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProfileModel(
            UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }

        [BindProperty]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Pasport Data")]
            public string PasportData { get; set; }
        }

        private async Task LoadAsync(Users user)
        {
            var Username = user.UserName;
            var customers = _applicationDbContext.Customers.Where(c => c.Id == user.Id)
                .FirstOrDefault();

            Input = new InputModel
            {
                PhoneNumber = user.PhoneNumber
            };
            if (customers != null)
            {
                Input.PasportData = customers.PasportData;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _applicationDbContext.Users
            .Include(e => e.Customers)
            .FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);


            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = user.PhoneNumber;
            if(user.Customers == null)
            {
                await _userManager.AddToRoleAsync(user, "Customer");

                user.Customers = new Customers()
                {
                    Id = user.Id,
                    PasportData = Input.PasportData
                };
                _applicationDbContext.Add(user.Customers);
                _applicationDbContext.SaveChanges();
            }
            else if(user.Customers.PasportData != Input.PasportData)
            {
                user.Customers.PasportData = Input.PasportData;
                _applicationDbContext.Update(user.Customers);
                _applicationDbContext.SaveChanges();
            }

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
