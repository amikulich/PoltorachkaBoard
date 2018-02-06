using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poltorachka.Pages
{
    public class FactEditModel : PageModel
    {
        public string Title { get; set; }

        public void OnGet()
        {
            Title = "f";
        }
    }
}