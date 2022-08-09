using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using AtlasDTO;
using AtlasMVCAPI.Models;

namespace AtlasMVCAPI.Controllers
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
        [HttpPost]
        public ActionResult AddToCart(string productID) // , string returnUrl
        {
            ItemDAC db = new ItemDAC();
            ItemVO product = db.GetProductInfo(productID);

            if (product != null) // 조회된 제품번호가 존재한다면
            {
                // 카트를 가져옵니다.
                Cart cart = GetCart();
                
                Session["intFlag"] = cart.AddOnceItem(product, 1);
                Session["itemName"] = product.ItemName;
                Session["Cart"] = cart;
            }
            // ViewData를 Product/List에 보내주고 싶은데 어쩌지?
            // [HttpGet]을 써야되나? 안될걸?
            return RedirectToAction("List", "Product");
        }
        private Cart GetCart()
        {
            // Session 정보에 있는 Cart를 가져온다 
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}