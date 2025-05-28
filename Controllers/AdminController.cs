using System.Linq;
using System.Web.Mvc;
using eUseControl.BusinesLogic.DBModel.Seed;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using WebApplication4.Attributes;

namespace WebApplication4.Controllers
{
     
     public class AdminController : Controller
     {
 
          public ActionResult Donatii()
          {
               return View();
          }

          public ActionResult Adauga()
          {
               return View();
          }

          public ActionResult FiseAdoptie()
          {
               return View();
          }

          public ActionResult About()
          {
               return RedirectToAction("About", "Home");
          }

          [AccessRoles(URole.Admin, URole.User, URole.Donator)] // dacă ai un atribut personalizat
          public ActionResult Profil()
          {
               var user = Session["_SessionObject"] as UserMinimal;
               if (user == null) return RedirectToAction("Index", "Login");

               using (var db = new UserContext())
               {
                    var fullUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
                    if (fullUser == null) return RedirectToAction("Index", "Login");

                    return View(fullUser);
               }
          }


          public ActionResult Logout()
          {
               Session.Clear();
               return RedirectToAction("Index", "Home");
          }
     }
}

