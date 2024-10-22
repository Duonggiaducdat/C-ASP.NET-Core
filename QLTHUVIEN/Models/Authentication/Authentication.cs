using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace QLTHUVIEN.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuted( ActionExecutedContext context )
        {
            if (context.HttpContext.Session.GetString("Username") == null)
            {
                context.Result = new RedirectToActionResult("Login", "TaiKhoan", null);
            }
        }
    }
}
