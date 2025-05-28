using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Attributes 
{
     public class AdminModAttribute : ActionFilterAttribute
     {
          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var role = HttpContext.Current.Session["UserRole"];

               if (role == null || role.ToString() != "Admin")
               {
                    // Redirectează spre pagina de Login sau acces interzis
                    filterContext.Result = new RedirectResult("/Login");
               }

               base.OnActionExecuting(filterContext);
          }
     }
}
