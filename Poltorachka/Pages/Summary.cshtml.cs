using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poltorachka.Domain;

namespace Poltorachka.Pages
{
    public class SummaryModel : PageModel
    {
        private readonly IFactSummaryQuery summaryQuery;

        public SummaryModel(IFactSummaryQuery summaryQuery)
        {
            this.summaryQuery = summaryQuery;
        }

        [BindProperty]
        public FactSummary Summary { get; set; }

        public void OnGet()
        {
            Summary = summaryQuery.Execute();
        }
    }
}
