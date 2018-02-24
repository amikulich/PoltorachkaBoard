using Microsoft.AspNetCore.Mvc;
using Poltorachka.Domain;
using Poltorachka.Models;
using Poltorachka.Services;

namespace Poltorachka.Pages.Facts
{
    public class FactEditModel : PageViewModelBase
    {
        private readonly IFactService _factService;
        private readonly IIndividualsQuery _individualsQuery;

        public FactEditModel(IFactService factService, IIndividualsQuery individualsQuery)
        {
            _factService = factService;
            _individualsQuery = individualsQuery;
        }

        public bool ApproveAllowed { get; private set; }

        public bool DeclineAllowed { get; private set; }

        public string ApproveClarification { get; private set; }

        public string DeclineClarification { get; private set; }

        [BindProperty]
        public FactEditViewModel Fact { get; set; }

        public void OnGet(int id)
        {
            Fact = _factService.Get(id);

            if (Fact.Status != FactStatusViewModel.Pending)
            {
                RedirectToPage("Index");
            }

            var indId = _individualsQuery.Execute(UserId).IndId;

            ApproveAllowed = Fact.WinnerId != indId
                                && Fact.CreatorId != indId;
            DeclineAllowed = Fact.LoserId != indId;

            ApproveClarification = ApproveAllowed ? string.Empty : "Find another person to approve";
            DeclineClarification = DeclineAllowed ? string.Empty : "Find another person to decline";
        }

        public ActionResult OnPostApprove(int id)
        {
            _factService.Update(id, UserId, FactStatusViewModel.Approved);

            return RedirectToPage("/Index");
        }

        public ActionResult OnPostDecline(int id)
        {
            _factService.Update(id, UserId, FactStatusViewModel.Canceled);

            return RedirectToPage("/Index");
        }
    }
}