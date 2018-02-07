using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poltorachka.Domain;

namespace Poltorachka.Pages
{
    public class FactEditModel : PageModel
    {
        private readonly IFactRepository factRepository;
        private readonly IIndividualsQuery individualsQuery;

        public FactEditModel(IFactRepository factRepository, IIndividualsQuery individualsQuery)
        {
            this.factRepository = factRepository;
            this.individualsQuery = individualsQuery;
        }

        public bool ApproveAllowed { get; private set; }

        public bool DeclineAllowed { get; private set; }

        public string ApproveClarification { get; private set; }

        public string DeclineClarification { get; private set; }

        [BindProperty]
        public Fact Fact { get; set; }

        public void OnGet(int id)
        {
            var currentUser = individualsQuery.Execute().Single(u => User.Claims.Single(c => c.Type == "name").Value == u.Email).Name;
            Fact = factRepository.GetById(id);

            if (Fact.Status != FactStatus.Registered)
            {
                RedirectToPage("Index");
            }

            ApproveAllowed = Fact.WinnerName != currentUser
                                && Fact.CreatorName != currentUser;
            DeclineAllowed = Fact.LoserName != currentUser;

            ApproveClarification = ApproveAllowed ? string.Empty : "Find another person to approve";
            DeclineClarification = DeclineAllowed ? string.Empty : "Find another person to decline";
        }

        public void OnPostApprove()
        {
            var currentUser = individualsQuery.Execute().Single(u => User.Claims.Single(c => c.Type == "name").Value == u.Email).Name;
            Fact.Approve(currentUser);
            factRepository.Save(Fact);

            RedirectToPage("Index");
        }

        public void OnPostDecline()
        {
            var currentUser = individualsQuery.Execute().Single(u => User.Claims.Single(c => c.Type == "name").Value == u.Email).Name;
            Fact.Decline(currentUser);
            factRepository.Save(Fact);

            RedirectToPage("Index");
        }
    }
}