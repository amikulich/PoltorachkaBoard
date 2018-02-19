using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poltorachka.Domain;
using Poltorachka.Models;
using Poltorachka.Services;

namespace Poltorachka.Pages.Facts
{
    public class CreateFactModel : PageModel
    {
        private readonly IFactService factService;
        private readonly IIndividualsQuery individualsQuery;

        public CreateFactModel(IFactService factService, IIndividualsQuery individualsQuery)
        {
            this.factService = factService;
            this.individualsQuery = individualsQuery;
        }

        [BindProperty]
        public CreateFactViewModel Fact { get; set; }

        public SelectList UserNames { get; set; }

        public void OnGet()
        {
            UserNames = new SelectList(individualsQuery.Execute().Select(i => i.Name));
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var creatorName = individualsQuery.Execute().Single(u => User.Identity.Name == u.UserName).Name;

            factService.Create(Fact.WinnerName, Fact.LoserName, Fact.Score, Fact.Description, creatorName);

            return RedirectToPage("/Index");
        }
    }
}