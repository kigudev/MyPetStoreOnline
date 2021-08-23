using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPNETIdentity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPetStoreOnline.Entities;

namespace ASPNETIdentity.Areas.Identity.Pages.MyAccount
{
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public ExternalLoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public string Provider { get; set; }

        public IActionResult OnPost()
        {
            // properties se encarga de configurar el redireccionamiento hacia el login externo
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl = "/Index" });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(Provider, redirectUrl);
            return new ChallengeResult(Provider, properties); // redirecciona al usuario al provedor de login externo
        }

        // se llama cuando el proveedor externo regresa la petición
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl, string remoteError)
        {
            // tiene toda la info que nos da facebook de la persona que inicia sesión
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if(info == null)
            {
                return RedirectToPage("/Login");
            }

            // intentamos iniciar sesión en nuestro sistema con la información que nos dieron
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);

            if (result.Succeeded)
            {
                // sesión iniciada, mandamos a home
                return RedirectToPage("/Index");
            }
            else
            {
                // creamos un usuario con la información que nos dieron
                if(info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                    var user = new ApplicationUser
                    {
                        Email = email,
                        UserName = email
                    };

                    var resultAccount = await _userManager.CreateAsync(user);

                    if (resultAccount.Succeeded)
                    {
                        resultAccount = await _userManager.AddLoginAsync(user, info);

                        if (resultAccount.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, true, info.LoginProvider);

                            return RedirectToPage("/Index");
                        }
                    }
                }
               
            }

            return RedirectToPage("/AccessDenied");
        }
    }
}
