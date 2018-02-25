using Microsoft.AspNetCore.Mvc.Filters;

namespace Poltorachka.Web.Filters
{
    public class NotFoundFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IPagesRedirectHelper _pagesRedirectHelper;

        public NotFoundFilterAttribute(IPagesRedirectHelper pagesRedirectHelper)
        {
            _pagesRedirectHelper = pagesRedirectHelper;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is PageNotFoundException)
            {
                context.Result = _pagesRedirectHelper.RedirectToDefault();
            }

            base.OnException(context);
        }
    }
}
