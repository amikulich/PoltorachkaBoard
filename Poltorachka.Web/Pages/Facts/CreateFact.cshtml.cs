using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poltorachka.Web.Models;
using Poltorachka.Web.Services;

namespace Poltorachka.Web.Pages.Facts
{
    public class CreateFactModel : PageViewModelBase
    {
        private readonly IFactAppService _factService;
        private readonly IIndividualsAppService _individualsService;

        public CreateFactModel(IFactAppService factService, IIndividualsAppService individualsService)
        {
            _factService = factService;
            _individualsService = individualsService;
        }

        [BindProperty]
        public FactCreateViewModel Fact { get; set; }

        public string GenericErrorMessage { get; private set; }

        public SelectList Individuals { get; set; }

        public void OnGet()
        {
            Individuals = new SelectList(_individualsService.Get(), nameof(IndividualViewModel.IndId), nameof(IndividualViewModel.Name));
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet();

                return Page();
            }

            try
            {
                _factService.Create(1, Fact.WinnerId, Fact.LoserId, UserId, Fact.Score, Fact.Description);

                return RedirectToPage("/Index");
            }
            catch (AppServiceException exception)
            {
                if (exception.Message == "Month limit exceeded for this user")
                {
                    GenericErrorMessage = exception.Message;

                    OnGet();

                    return Page();
                }

                throw;
            }
        }
    }
}