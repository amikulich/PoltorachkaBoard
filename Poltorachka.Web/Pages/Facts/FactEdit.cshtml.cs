using Microsoft.AspNetCore.Mvc;
using Poltorachka.Domain.Individuals;
using Poltorachka.Web.Models;
using Poltorachka.Web.Services;

namespace Poltorachka.Web.Pages.Facts
{
    public class FactEditModel : PageViewModelBase
    {
        private readonly IPagesRedirectHelper _redirectHelper;
        private readonly IFactAppService _factService;
        private readonly IIndividualsQuery _individualsQuery;

        public FactEditModel(IPagesRedirectHelper redirectHelper,
            IFactAppService factService, 
            IIndividualsQuery individualsQuery)
        {
            _redirectHelper = redirectHelper;
            _factService = factService;
            _individualsQuery = individualsQuery;
        }

        public bool ApproveAllowed { get; private set; }

        public bool DeclineAllowed { get; private set; }

        public string ApproveClarification { get; private set; }

        public string DeclineClarification { get; private set; }

        [BindProperty]
        public FactEditViewModel Fact { get; set; }

        public ActionResult OnGet(int id)
        {
            Fact = _factService.Get(id);

            if (Fact.Status != FactStatusViewModel.Pending)
            {
                _redirectHelper.RedirectToDefault(UserId);
            }

            var indId = _individualsQuery.Execute(UserId).IndId;

            ApproveAllowed = Fact.WinnerId != indId
                                && Fact.CreatorId != indId;
            DeclineAllowed = Fact.LoserId != indId;

            ApproveClarification = ApproveAllowed ? string.Empty : "Find another person to approve";
            DeclineClarification = DeclineAllowed ? string.Empty : "Find another person to decline";

            return Page();
        }

        public IActionResult OnPostApprove(int id)
        {
            _factService.Update(id, UserId, FactStatusViewModel.Approved);

            return _redirectHelper.RedirectToDefault(UserId);
        }

        public IActionResult OnPostDecline(int id)
        {
            _factService.Update(id, UserId, FactStatusViewModel.Canceled);

            return _redirectHelper.RedirectToDefault(UserId);
        }
    }
}