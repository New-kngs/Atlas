using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using AtlasDTO;
using AtlasMVCAPI.Models;

namespace AtlasMVCAPI.WebControllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult List()
        {
            ItemDAC db = new ItemDAC();
            List<ItemVO> model = db.GetProduct();
            // db.Dispose();
            return View(model);
        }
    }
}