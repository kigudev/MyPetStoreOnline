using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPetStoreOnline.Entities;

namespace MyPetStore.Web.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public string Id { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public string Errors { get; set; }

        public async Task OnGetAsync() => Users = await _userManager.Users.ToListAsync();

        public async Task OnPostAsync()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);

                if(user != null)
                await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
            }
            await OnGetAsync();
        }
    }
}
