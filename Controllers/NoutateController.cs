using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;
using eUseControl.BusinesLogic.DBModel.Seed;
using eUseControl.Domain.Enums;
using WebApplication4.Attributes;

namespace WebApplication4.Controllers
{
     [AccessRoles(URole.Admin)]
     public class NoutateController : Controller
     {
          private readonly UserContext db = new UserContext();

          [HttpGet]
          public ActionResult Adauga()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Adauga(PostareNoua model, HttpPostedFileBase img)
          {
               if (ModelState.IsValid)
               {
                    if (img != null && img.ContentLength > 0)
                    {
                         var fileName = Path.GetFileName(img.FileName);
                         var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                         img.SaveAs(path);
                         model.Imagine = "~/Content/images/" + fileName;
                    }

                    model.Data = DateTime.Now;

                    db.PostariBlog.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Blog", "Home");
               }

               return View(model);
          }
     }
}
