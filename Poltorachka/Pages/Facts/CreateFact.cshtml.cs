using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poltorachka.Models;
using Poltorachka.Services;

namespace Poltorachka.Pages.Facts
{
    public class CreateFactModel : PageViewModelBase
    {
        private readonly IFactService _factService;
        private readonly IIndividualsService _individualsService;

        public CreateFactModel(IFactService factService, IIndividualsService individualsService)
        {
            _factService = factService;
            _individualsService = individualsService;
        }

        [BindProperty]
        public FactCreateViewModel Fact { get; set; }

        public SelectList Individuals { get; set; }

        public void OnGet()
        {
            Individuals = new SelectList(_individualsService.Get(), nameof(IndividualViewModel.IndId), nameof(IndividualViewModel.Name));
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _factService.Create(Fact.WinnerId, Fact.LoserId, UserId, Fact.Score, Fact.Description);

            return RedirectToPage("/Index");
        }
    }
}