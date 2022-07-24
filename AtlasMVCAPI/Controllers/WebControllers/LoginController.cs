using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers.WebControllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Customer()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Login(string UserID, string UserPWD)
        //{
        //    Cuso user = db.Login(UserID, UserPWD);
        //    if (user != null)
        //    {
        //        Session["UserInfo"] = user;
        //        Session["UserName"] = user.UserName;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //}


        //public ActionResult Logout()
        //{
        //    Session["UserInfo"] = null;
        //    Session.Clear();

        //    return RedirectToAction("Index", "Home");
        //}
    }
}