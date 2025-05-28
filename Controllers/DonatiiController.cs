using System;
using System.Linq;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;
using eUseControl.BusinessLogic;
using eUseControl.BusinesLogic.DBModel.Seed;
using WebApplication4.Attributes;
using eUseControl.Domain.Enums;

namespace WebApplication4.Controllers
{
     public class DonatiiController : Controller
     {
          private readonly UserContext db = new UserContext();

          // GET: Formular Donator
          [HttpGet]
          public ActionResult Formular()
          {
               return View();
          }

          // POST: Salvează Donația
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Formular(Donatii model, string numarCard)
          {
               if (ModelState.IsValid)
               {
                    model.DataDonatiei = DateTime.Now;
                    db.Donatii.Add(model);
                    db.SaveChanges();
                    TempData["MesajSucces"] = "Donația a fost înregistrată!";
                    return RedirectToAction("Formular");
               }

               return View(model);
          }

          // Vizualizare Istoric Donator
          [AccessRoles(URole.Admin, URole.Donator)]
          public ActionResult Istoric()
          {
               var user = (UserMinimal)Session["_SessionObject"];
               if (user == null || string.IsNullOrEmpty(user.Email))
               {
                    TempData["Error"] = "Nu ești autentificat corect.";
                    return RedirectToAction("Index", "Login");
               }

               var donatii = db.Donatii.Where(d => d.Email == user.Email).ToList();
               return View(donatii);
          }




          // Vizualizare pentru Admin
          [AccessRoles(URole.Admin)]
          public ActionResult Lista()
          {
               var toate = db.Donatii.ToList();
               return View(toate);
          }


     }
}
