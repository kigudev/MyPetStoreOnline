using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPNETIdentity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPetStoreOnline.Entities;

namespace ASPNETIdentity.Areas.Identity.Pages.MyAccount
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Email,
                    Email = Input.Email
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, Input.Role);
                    if (roleResult.Succeeded)
                    {
                        //await _signManager.SignInAsync(user, true);
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        // si no se puede agregar el rol al usuario vamos a eliminar el usuario recien
                        // creado porque no se cumplió con lo que recibimos del formulario.
                        await _userManager.DeleteAsync(user);
                        ModelState.AddModelError(string.Empty, "No se pudo agregar al usuario");
                    }
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }

            return Page();
        }


        public class InputModel
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            public string Role { get; set; }
        }
    }


}
