using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poltorachka.Web
{
    public class PageViewModelBase : PageModel
    {
        protected Guid UserId
        {
            get
            {
                var id = User?.Claims.SingleOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                return !string.IsNullOrEmpty(id) ? Guid.Parse(id) : Guid.Empty;
            }
        }
    }
}
