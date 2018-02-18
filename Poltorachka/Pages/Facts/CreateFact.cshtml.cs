using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poltorachka.Domain;

namespace Poltorachka.Pages.Facts
{
    public class CreateFactModel : PageModel
    {
        private readonly IFactRepository factRepository;

        private readonly IIndividualsQuery individualsQuery;

        public CreateFactModel(IFactRepository factRepository, IIndividualsQuery individualsQuery)
        {
            this.factRepository = factRepository;
            this.individualsQuery = individualsQuery;
        }

        [BindProperty]
        public Models.CreateFactModel Fact { get; set; }

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

            factRepository.Save(new Fact(Fact.WinnerName, Fact.LoserName, creatorName, Fact.Score, Fact.Description));

            return RedirectToPage("/Index");
        }
    }
}