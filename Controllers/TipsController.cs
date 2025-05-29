using eUseControl.Domain.Entities.User;
using System.Data.Entity;
using System.Web.Mvc;
using System;
using eUseControl.BusinesLogic.DBModel.Seed;
using System.Linq;

public class TipsController : Controller
{
      private readonly UserContext db = new UserContext();

     public ActionResult Index()
     {
          var sfaturi = db.Sfaturi.OrderByDescending(s => s.DataAdaugarii).ToList();
          return View(sfaturi);
     }

     [Authorize(Roles = "Admin")]
     public ActionResult Create()
     {
          return View();
     }

     [HttpPost]
     [Authorize(Roles = "Admin")]
     public ActionResult Create(TipSfat model)
     {
          if (ModelState.IsValid)
          {
               model.DataAdaugarii = DateTime.Now;
               model.Autor = User.Identity.Name;
               db.Sfaturi.Add(model);
               db.SaveChanges();
               return RedirectToAction("Index");
          }
          return View(model);
     }

     [Authorize(Roles = "Admin")]
     public ActionResult Edit(int id)
     {
          var sfat = db.Sfaturi.Find(id);
          return View(sfat);
     }

     [HttpPost]
     [Authorize(Roles = "Admin")]
     public ActionResult Edit(TipSfat model)
     {
          if (ModelState.IsValid)
          {
               db.Entry(model).State = EntityState.Modified;
               db.SaveChanges();
               return RedirectToAction("Index");
          }
          return View(model);
     }

     [Authorize(Roles = "Admin")]
     public ActionResult Delete(int id)
     {
          var sfat = db.Sfaturi.Find(id);
          return View(sfat);
     }

     [HttpPost, ActionName("Delete")]
     [Authorize(Roles = "Admin")]
     public ActionResult DeleteConfirmed(int id)
     {
          var sfat = db.Sfaturi.Find(id);
          db.Sfaturi.Remove(sfat);
          db.SaveChanges();
          return RedirectToAction("Index");
     }
}
