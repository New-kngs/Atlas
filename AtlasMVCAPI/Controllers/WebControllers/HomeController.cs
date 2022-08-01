using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers
{ 
    public class HomeController : Controller
    {
        // GET: 홈페이지
        public ActionResult Index()
        {
            return View();
        }
    }
}