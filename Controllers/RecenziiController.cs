using System;
using System.Linq;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;
using eUseControl.BusinesLogic.DBModel.Seed;
using eUseControl.Domain.Enums;
using WebApplication4.Attributes;

namespace WebApplication4.Controllers
{
     public class RecenziiController : Controller
     {
          private readonly UserContext db = new UserContext();

          [AccessRoles(URole.User, URole.Donator)]
          public ActionResult AdaugaRecenzie()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          [AccessRoles(URole.User, URole.Donator)]
          public ActionResult AdaugaRecenzie(Recenzie model)
          {
               var user = (UserMinimal)Session["_SessionObject"];
               if (user == null)
               {
                    return RedirectToAction("Index", "Login");
               }

               model.Autor = user.Username;
               model.Data = DateTime.Now;

               db.Recenzii.Add(model);
               db.SaveChanges();

               TempData["Success"] = "Recenzia a fost adăugată cu succes!";
               return RedirectToAction("Index", "Home");
          }

          [HttpGet]
          [AccessRoles(URole.User, URole.Donator)]
          public ActionResult EditRecenzie(int id)
          {
               var recenzie = db.Recenzii.FirstOrDefault(r => r.Id == id);
               if (recenzie == null)
               {
                    return HttpNotFound();
               }

               var user = (UserMinimal)Session["_SessionObject"];
               if (recenzie.Autor != user.Username)
               {
                    TempData["Error"] = "Nu ai voie să modifici această recenzie.";
                    return RedirectToAction("Index", "Home");
               }

               return View(recenzie);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          [AccessRoles(URole.User, URole.Donator)]
          public ActionResult EditRecenzie(Recenzie model)
          {
               var recenzie = db.Recenzii.FirstOrDefault(r => r.Id == model.Id);
               if (recenzie == null)
               {
                    return HttpNotFound();
               }

               var user = (UserMinimal)Session["_SessionObject"];
               if (recenzie.Autor != user.Username)
               {
                    TempData["Error"] = "Nu ai voie să modifici această recenzie.";
                    return RedirectToAction("Index", "Home");
               }

               // Actualizează textul și data
               recenzie.Text = model.Text;
               recenzie.Data = DateTime.Now;

               db.SaveChanges();
               TempData["Success"] = "Recenzia a fost actualizată cu succes!";
               return RedirectToAction("Index", "Home");
          }

     }
}
