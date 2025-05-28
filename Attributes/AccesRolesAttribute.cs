using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;

namespace WebApplication4.Attributes
{
     public class AccessRolesAttribute : ActionFilterAttribute
     {
          private readonly URole[] _allowedRoles;

          public AccessRolesAttribute(params URole[] roles)
          {
               _allowedRoles = roles;
          }

          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var user = (UserMinimal)HttpContext.Current.Session["_SessionObject"];

               if (user == null || !_allowedRoles.Contains(user.Level))
               {
                    filterContext.Result = new RedirectResult("/Login");
               }
          }
     }
}
