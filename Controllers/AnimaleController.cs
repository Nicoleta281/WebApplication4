using System.Linq;
using System.Web.Mvc;
using eUseControl.BusinesLogic.DBModel.Seed;
using System.Web;
using eUseControl.Domain.Enums;
using WebApplication4.Attributes;
using eUseControl.Domain.Entities.Animale;


public class AnimaleController : Controller
{
     private UserContext db = new UserContext();

     // Listă animale pentru Home/Index.cshtml
     public ActionResult GetAnimale()
     {
          var animale = db.Animale.ToList();
          return PartialView("_ListaAnimale", animale);
     }

     // Admin CRUD
     [AccessRoles(URole.Admin)]
     public ActionResult Manage()
     {
          return View(db.Animale.ToList());
     }

     [HttpGet]
     [AccessRoles(URole.Admin)]
     public ActionResult Create() { return View(); }

     [HttpPost]
     [AccessRoles(URole.Admin)]
     public ActionResult Create(Animal a, HttpPostedFileBase poza)
     {
          if (poza != null && poza.ContentLength > 0)
          {
               var path = Server.MapPath("~/Content/images/" + poza.FileName);
               poza.SaveAs(path);
               a.Imagine = "/Content/images/" + poza.FileName;
          }
          db.Animale.Add(a);
          db.SaveChanges();
          return RedirectToAction("Manage");
     }

     [AccessRoles(URole.Admin)]
     public ActionResult Edit(int id)
     {
          var animal = db.Animale.Find(id);
          if (animal == null)
          {
               return HttpNotFound();
          }
          return View(animal);
     }


     [HttpPost]
     [AccessRoles(URole.Admin)]
     public ActionResult Edit(Animal a, HttpPostedFileBase poza)
     {
          var animal = db.Animale.Find(a.Id);
          if (poza != null)
          {
               var path = Server.MapPath("~/Content/images/" + poza.FileName);
               poza.SaveAs(path);
               animal.Imagine = "/Content/images/" + poza.FileName;
          }
          animal.Nume = a.Nume;
          animal.Specie = a.Specie;
          animal.Varsta = a.Varsta;
          animal.Descriere = a.Descriere;
          db.SaveChanges();
          return RedirectToAction("Manage");
     }

     [AccessRoles(URole.Admin)]
     public ActionResult Delete(int id)
     {
          var animal = db.Animale.Find(id);
          db.Animale.Remove(animal);
          db.SaveChanges();
          return RedirectToAction("Manage");
     }
}
