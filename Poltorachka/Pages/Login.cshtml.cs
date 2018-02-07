using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poltorachka.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; }

        [Authorize]
        public void Login()
        {
            //UserName = User.Identity.Name;
        }
    }
}