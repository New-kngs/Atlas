using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers.WebControllers
{
    public class CartController : Controller
    {
        // GET: 장바구니
        public ActionResult Index()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult Basket()
        {
            return View();
        }
    }
}