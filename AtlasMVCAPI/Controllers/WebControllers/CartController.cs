using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers
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
        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        // 상품 상세 팝업창
        public PartialViewResult PopUp(string productID)
        {
            ItemDAC db = new ItemDAC();
            BOMDAC bomDB = new BOMDAC();

            ProductPopUpModel model = new ProductPopUpModel
            {
                Product = db.GetProductInfo(productID),
                BOM = bomDB.GetBOMForwardList(productID)
                // GetBOMForwardList
            };

            // (List<OrderVO>, List<OrderDetailVO>) model = db.GetOrderDetails("쿼리문 잘못 짯다");
            return PartialView(model);
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
                Session["Cart"] = cart;
            }
            // 장바구니 페이지로 이동
            return RedirectToAction("Basket");
        }
        [HttpPost]
        public ActionResult RemoveToCart(string productID)
        {
            Cart cart = GetCart();
            cart.RemoveItem(productID);
            Session["Cart"] = cart;
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

        [HttpPost]
        public ActionResult ButtonOrder()
        {
            Cart cart = Session["Cart"] as Cart;
            StringBuilder sbItemId = new StringBuilder();
            StringBuilder sbQty = new StringBuilder();
            if (cart != null)
            {
                LoginVO loginUser = (LoginVO)Session["LoginInfo"];
                Cart CartDetail = (Cart)Session["Cart"];

                foreach (CartLine cartRow in CartDetail.Lines)
                {
                    sbItemId.Append(cartRow.Product.ItemID);
                    sbQty.Append(cartRow.Qty);

                    sbItemId.Append(',');
                    sbQty.Append(',');
                }
                OrderDAC db = new OrderDAC();
                db.CreateOrder(loginUser.CustomerID, loginUser.EmpName, sbItemId.ToString().TrimEnd(','), sbQty.ToString().TrimEnd(','));
            }
            return null;
        }
    }
}