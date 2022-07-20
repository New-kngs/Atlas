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
        // GET: 전체 제품 목록을 조회
        // https://localhost:44391/Product/List
        public ActionResult List(int page=1)
        {
            ItemDAC db = new ItemDAC();
            List<ItemVO> model = db.GetProduct();
            //db.Dispose();
            return View(model);
        }
    }
}