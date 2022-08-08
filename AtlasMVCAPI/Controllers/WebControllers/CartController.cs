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
        public ActionResult AddToCart(string productID) // , string returnUrl
        {
            //if (!string.IsNullOrEmpty(returnUrl))
            //{
            //    Session["returnUrl"] = returnUrl;
            //}

            ItemDAC db = new ItemDAC();
            ItemVO product = db.GetProductInfo(productID);

            if (product != null) // 조회된 제품이 없다면
            {
                // 장바구니 추가
                Cart cart = GetCart();
                cart.AddOnceItem(product, 1);
                Session["Cart"] = cart;
            }
            else
            {
                // 장바구니에는 제품 1개만 담을 수 있다.
            }
            // 장바구니 페이지로 이동 return RedirectToAction("Basket");
            return RedirectToAction("Product", "List");
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
            Session["Cart"] = null; // 주문이 완료되었으므로, 카트에 담긴 상품을 비웁니다.
            return RedirectToAction("History","OrderWeb");
        }

        public ActionResult UpdateQty(string UP_Prod, string UP_Qty)
        {
            Cart cart = Session["Cart"] as Cart;
            string prod_id = UP_Prod;
            int qty = Convert.ToInt32(UP_Qty);

            CartLine line = cart.Lines.Where<CartLine>((p) => p.Product.ItemID.Equals(prod_id)).FirstOrDefault();
            if (line != null)
            {
                line.Qty = qty;
                Session["Cart"] = cart;
            }
            return RedirectToAction("Basket");
        }
    }
}