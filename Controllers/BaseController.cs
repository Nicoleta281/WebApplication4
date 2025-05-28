using eUseControl.BusinesLogic.Interfaces;
using eUseControl.BusinessLogic;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;
using System;
using WebApplication4.Extensions;
namespace WebApplication4.Controllers
{
     public class BaseController : Controller
     {
          private readonly ISession _session;

          public BaseController()
          {
               BusinessLogic bl = new BusinessLogic();
               _session = bl.GetSessionBL();
          }

          // Metodă publică ce va putea fi folosită de toți controllerii ce moștenesc BaseController
          public void SessionStatus(UDTable profile)
          {
               var apiCookie = Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var userProfile = _session.GetUserByCookie(apiCookie.Value);
                    if (userProfile != null && userProfile.ExpireTime > DateTime.Now)
                    {
                         UserMinimal userMinimal = new UserMinimal
                         {
                              Username = userProfile.Username,
                              Level = userProfile.Level,
                              Email = userProfile.Email
                         };
                         System.Web.HttpContext.Current.SetMySessionObject(userMinimal);
                         this.HttpContext.Session["LoginStatus"] = "login";
                    }

                    else
                    {
                         this.HttpContext.Session.Clear();
                         var cookie = Request.Cookies["X-KEY"];
                         if (cookie != null)
                         {
                              cookie.Expires = DateTime.Now.AddDays(-1);
                              Response.Cookies.Add(cookie);
                         }
                         this.HttpContext.Session["LoginStatus"] = "logout";
                    }
               }
               else
               {
                    this.HttpContext.Session["LoginStatus"] = "logout";
               }
          }


     }
}
