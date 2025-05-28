using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.User;
using eUseControl.BusinessLogic;
using eUseControl.Helpers;
using eUseControl.Domain.Enums;
using eUseControl.BusinesLogic.DBModel.Seed;

namespace WebApplication4.Controllers
{
     public class LoginController : Controller
     {
          private readonly SessionBL _session = new SessionBL();

          // GET: Login
          public ActionResult Index()
          {
               return View(new ULoginData());
          }

          // POST: Login
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Index(ULoginData login)
          {
               if (ModelState.IsValid)
               {
                    var userLogin = _session.UserLogin(new ULoginData
                    {
                         Credential = login.Credential,
                         Password = login.Password,
                         LoginIp = Request.UserHostAddress,
                         LoginDataTime = DateTime.Now
                    });

                    if (userLogin.Status)
                    {
                         // Generează cookie
                         HttpCookie cookie = _session.GenCookie(login.Credential);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                         // Setează sesiunea completă
                         var sessionUser = new UserMinimal
                         {
                              Id = userLogin.User.Id,
                              Username = userLogin.User.Username,
                              Email = userLogin.User.Email,
                              Level = userLogin.User.Level
                         };

                         Session["_SessionObject"] = sessionUser;
                         Session["UserRole"] = userLogin.User.Level.ToString();
                         Session["UserEmail"] = userLogin.User.Email;

                         Session["Login"] = true;

                         // Redirecționare pe rol
                         return RedirectToAction("Profil", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", userLogin.StatusMsg);
                    }
               }

               return View(login);
          }

          // GET: Register
          public ActionResult Register()
          {
               return View(new ULoginData());
          }

          // POST: Register
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Register(ULoginData data)
          {
               if (!ModelState.IsValid)
               {
                    ViewBag.Message = "Date invalide.";
                    return View(data);
               }

               using (var db = new UserContext())
               {
                    var existent = db.Users.FirstOrDefault(u => u.Email == data.Email);
                    if (existent != null)
                    {
                         ViewBag.Message = "Emailul există deja.";
                         return View(data);
                    }

                    // Crează utilizator
                    var newUser = new UDTable
                    {
                         FirstName = data.FirstName,
                         LastName = data.LastName,
                         Email = data.Email,
                         Username = data.Email, // folosim email ca username
                         Password = LoginHelper.HashGen(data.Password),
                         Level = URole.User,
                         LastLogin = DateTime.Now,
                         LastIp = Request.UserHostAddress
                    };

                    try
                    {
                         db.Users.Add(newUser);
                         db.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                    {
                         foreach (var eve in ex.EntityValidationErrors)
                         {
                              foreach (var ve in eve.ValidationErrors)
                              {
                                   ModelState.AddModelError("", $"Eroare la câmpul {ve.PropertyName}: {ve.ErrorMessage}");
                              }
                         }
                         return View(data);
                    }

                    // Setează sesiunea după înregistrare
                    var sessionUser = new UserMinimal
                    {
                         Id = newUser.Id,
                         Username = newUser.Email,
                         Email = newUser.Email,
                         Level = newUser.Level
                    };

                    Session["_SessionObject"] = sessionUser;
                    Session["UserRole"] = newUser.Level.ToString();
                    Session["Login"] = true;

                    return RedirectToAction("Index", "Home");
               }
          }

          // GET: ForgotPassword
          public ActionResult ForgotPassword()
          {
               return View();
          }

          // Logout
          public ActionResult Logout()
          {
               Session.Clear();
               return RedirectToAction("Index", "Home");
          }
     }
}
