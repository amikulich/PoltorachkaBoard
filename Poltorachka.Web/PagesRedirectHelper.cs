using System;
using Microsoft.AspNetCore.Mvc;

namespace Poltorachka.Web
{
    public interface IPagesRedirectHelper
    {
        IActionResult RedirectToDefault();

        IActionResult RedirectToDefault(Guid userId);
    }

    public class PagesRedirectHelper : IPagesRedirectHelper
    {
        public IActionResult RedirectToDefault()
        {
            return new RedirectToPageResult("/Index");
        }

        public IActionResult RedirectToDefault(Guid userId)
        {
            return new RedirectToPageResult("/Index");
        }
    }
}
