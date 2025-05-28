using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using eUseControl.Domain.Entities.Session;  // Pentru extensiile HttpContext
using System.Web;
using WebApplication4.Controllers;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.Animale;
using eUseControl.Domain.Enums;
using WebApplication4.Attributes;
using eUseControl.BusinesLogic.DBModel.Seed;

namespace WebApplication4.Controllers
{
    public class HomeController : BaseController
     {
          private readonly UserContext db = new UserContext();
          public ActionResult Index()
{
    SessionStatus(profile);

    ViewBag.Username = Session["User"];

    var recenzii = db.Recenzii.ToList();
    return View(recenzii);
}



          public ActionResult About()
        {
            return View();
        }
          public ActionResult Pet()
          {
               var animale = db.Animale.ToList(); // Obține animalele din baza de date
               return View(animale); // Trimite-le către Pet.cshtml
          }

          public ActionResult Team()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }

          public ActionResult Blog()
          {
               using (var db = new UserContext())
               {
                    var postari = db.PostariBlog.OrderByDescending(p => p.Data).ToList();
                    return View(postari);
               }
          }
          public ActionResult Tips()
        {
            return View();
        }


          private UDTable profile;



          [DonatorAccess]
          public ActionResult IstoricDonatii()
          {
               return View();
          }

          [DonatorAccess]
          public ActionResult Cazuri()
          {
               return View();
          }

          public ActionResult BlogSingle(int id)
          {
               using (var db = new UserContext())
               {
                    var post = db.PostariBlog.FirstOrDefault(p => p.Id == id);
                    if (post == null)
                    {
                         return HttpNotFound();
                    }
                    return View(post);
               }
          }

     }
}