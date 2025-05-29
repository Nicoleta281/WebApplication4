using System;
using System.Linq;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;
using eUseControl.BusinesLogic.DBModel.Seed;
using eUseControl.Domain.Enums;
using WebApplication4.Attributes;

namespace WebApplication4.Controllers
{
     public class AdoptieController : BaseController
     {
          private readonly UserContext db = new UserContext();

          // ✅ Doar adminul poate vedea toate cererile
          [AccessRoles(URole.Admin)]
          public ActionResult Index()
          {
               var cereri = db.AdoptieRequests.ToList();
               return View(cereri);
          }

          // ✅ Doar adminul poate schimba statusul
          [HttpPost]
          [AccessRoles(URole.Admin)]
          public ActionResult ActualizeazaStatus(int id, string status)
          {
               var cerere = db.AdoptieRequests.FirstOrDefault(c => c.Id == id);
               if (cerere != null)
               {
                    cerere.Status = status;
                    db.SaveChanges();
               }
               return RedirectToAction("Index");
          }

          // ✅ Doar utilizatorii logați (User sau Donator) pot trimite cerere
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult TrimiteCerere(AdoptieRequest model)
          {
               var user = Session["_SessionObject"] as UserMinimal;
               if (user == null || (user.Level != URole.User && user.Level != URole.Donator))
               {
                    TempData["Error"] = "Trebuie să fii logat ca utilizator sau donator pentru a trimite o cerere.";
                    return RedirectToAction("Index", "Home", new { animal = model.Animal });
               }

               try
               {
                    // Asigură-te că salvezi emailul utilizatorului logat!
                    model.Email = user.Email;
                    model.DataCerere = DateTime.Now;
                    model.Status = "În așteptare";

                    db.AdoptieRequests.Add(model);
                    db.SaveChanges();

                    TempData["Success"] = "Cererea a fost trimisă cu succes!";
                    return RedirectToAction("Index", "Home", new { animal = model.Animal, success = true });
               }
               catch (System.Data.Entity.Validation.DbEntityValidationException ex)
               {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                         foreach (var ve in eve.ValidationErrors)
                         {
                              System.Diagnostics.Debug.WriteLine($"Eroare la câmpul {ve.PropertyName}: {ve.ErrorMessage}");
                         }
                    }
                    TempData["Error"] = "A apărut o eroare la trimiterea cererii. Verifică datele.";
                    return RedirectToAction("Index", "Home", new { animal = model.Animal });
               }
          }

          // ✅ Utilizatorii își văd propriile cereri
          [AccessRoles(new URole[] { URole.User, URole.Donator })]


          public ActionResult CererileMele()
          {
               var user = (UserMinimal)Session["_SessionObject"];
               if (user == null)
               {
                    return RedirectToAction("Index", "Login");
               }

               var cereri = db.AdoptieRequests.Where(c => c.Email == user.Email).ToList();
               return View(cereri);
          }
     }
}
