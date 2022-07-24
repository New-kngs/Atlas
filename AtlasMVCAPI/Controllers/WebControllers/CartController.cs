using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers.WebControllers
{
    public class CartController : Controller
    {
        // GET: 장바구니 페이지
        public ActionResult Basket()
        {
            // line에 추가된 정보로 장바구니를 보여줌
            Cart cart = GetCart();
            return View(cart);
        }
        [HttpPost]
        public ActionResult AddToCart(string productID, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                Session["returnUrl"] = returnUrl;
            }

            ItemDAC db = new ItemDAC();
            ItemVO product = db.GetProductInfo(productID);

            if (product != null) // 조회된 제품이 없다면
            {
                // 장바구니 추가
                Cart cart = GetCart();
                cart.AddItem(product, 1);
            }
            // 장바구니 페이지로 이동
            return RedirectToAction("Basket");
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