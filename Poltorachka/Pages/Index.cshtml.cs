using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poltorachka.Models;
using Poltorachka.Services;

namespace Poltorachka.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFactsService factsService;

        public IndexModel(IFactsService factsService)
        {
            this.factsService = factsService;
        }

        public IEnumerable<FactViewModel> Facts { get; private set; }

        public void OnGet()
        {
            Facts = factsService.GetAll().OrderByDescending(f => f.Date);
        }
    }
}
