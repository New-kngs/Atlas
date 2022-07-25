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
        // https://localhost:44391/Product/List?page=1
        public ActionResult List(int page = 1)
        {
            ItemDAC db = new ItemDAC();
            List<ItemVO> list = db.GetProductListPage(page, 4);
            int total = db.GetProductTotalCount();
            // db.Dispose();
            ProductListViewModel model = new ProductListViewModel
            {
                Products = list,
                // TotalItems   총 데이터건수
                // ItemsPerPage 한 페이지당 목록 건수
                // CurrentPage  현재 페이지 번호
                Page = new PagingInfo { TotalItems = total, CurrentPage = page, ItemsPerPage = 4 }
            };
            return View(model);
        }
    }
}