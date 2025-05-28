using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Helpers;

namespace WebApplication4.Attributes
{
     public class DonatorAccessAttribute : ActionFilterAttribute
     {
          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var user = (UserMinimal)HttpContext.Current.Session["_SessionObject"];

               if (user == null || user.Level != URole.Donator)
               {
                    // Redirecționează către pagina de Login dacă nu are rol de Donator
                    filterContext.Result = new RedirectResult("/Login");
               }

               base.OnActionExecuting(filterContext);
          }
     }
}
