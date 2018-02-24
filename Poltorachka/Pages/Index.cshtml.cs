using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Poltorachka.Models;
using Poltorachka.Services;

namespace Poltorachka.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDashboardAppService _factsDashboardService;

        public IndexModel(IDashboardAppService factsDashboardService)
        {
            _factsDashboardService = factsDashboardService;
        }

        public IEnumerable<FactDashboardViewModel> Facts { get; private set; }

        public void OnGet()
        {
            Facts = _factsDashboardService.GetAll().OrderByDescending(f => f.Date);
        }
    }
}
