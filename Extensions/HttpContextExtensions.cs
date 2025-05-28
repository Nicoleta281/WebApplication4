using eUseControl.Domain.Entities.User;
using System.Web;
namespace WebApplication4.Extensions

{
     public static class HttpContextExtensions
     {
          public static UserMinimal GetMySessionObject(this HttpContext current)
          {
               return current.Session["_SessionObject"] as UserMinimal;
          }

          public static void SetMySessionObject(this HttpContext current, UserMinimal profile)
          {
               current.Session["_SessionObject"] = profile;
          }
     }
}
