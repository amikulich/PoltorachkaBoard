using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poltorachka.Web.Models;
using Poltorachka.Web.Services;

namespace Poltorachka.Web.Pages.Facts
{
    public class CreateFactModel : PageViewModelBase
    {
        private readonly IPagesRedirectHelper _pagesRedirectHelper;

        private readonly IFactAppService _factService;
        private readonly IIndividualsAppService _individualsService;

        public CreateFactModel(IPagesRedirectHelper pagesRedirectHelper, IFactAppService factService, IIndividualsAppService individualsService)
        {
            _pagesRedirectHelper = pagesRedirectHelper;
            _factService = factService;
            _individualsService = individualsService;
        }

        [BindProperty]
        public FactModel Fact { get; set; }

        public FactType Type { get; private set; }

        public string GenericErrorMessage { get; private set; }

        public SelectList Individuals { get; set; }

        public IActionResult OnGet(FactType type)
        {
            if (!Enum.IsDefined(typeof(FactType), type))
            {
                return _pagesRedirectHelper.RedirectToDefault(UserId);
            }

            Individuals = new SelectList(_individualsService.Get(), nameof(IndividualViewModel.IndId), nameof(IndividualViewModel.Name));
            Type = type;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return OnGet(Type);
            }

            try
            {
                _factService.Create(1, Fact.WinnerId, Fact.LoserId, UserId, Fact.Score, Fact.Description);

                return _pagesRedirectHelper.RedirectToDefault(UserId);
            }
            catch (AppServiceException exception)
            {
                if (exception.Message == "Month limit exceeded for this user")
                {
                    GenericErrorMessage = exception.Message;

                    return OnGet(Type);
                }

                throw;
            }
        }

        public enum FactType
        {
            Charge = 1,
            Donate = 2,
        }

        public class FactModel
        {
            [Range(1, int.MaxValue, ErrorMessage = "Нужно выбрать")]
            public int WinnerId { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Нужно выбрать")]
            public int LoserId { get; set; }

            [Range(1, 4, ErrorMessage = "Нельзя предъявить больше 4 за раз")]
            public byte Score { get; set; }

            [MaxLength(255, ErrorMessage = "Давай-ка до 255 буков")]
            public string Description { get; set; }
        }
    }
}