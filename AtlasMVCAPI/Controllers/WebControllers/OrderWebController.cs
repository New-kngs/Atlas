using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers.WebControllers
{
    public class OrderWebController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            //로그인을 했을때만 주문내역을 볼 수 있음
            if (Session["UserVO"] == null)
                return RedirectToAction("Lock", "Login");
            return View();
        }
    }
}