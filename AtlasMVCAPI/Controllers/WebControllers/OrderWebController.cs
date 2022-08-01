using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers
{
    public class OrderWebController : Controller
    {
        // GET: Order
        public ActionResult History()
        {
            //로그인을 했을때만 주문내역을 볼 수 있음
            if (Session["LoginInfo"] == null)
                return RedirectToAction("Lock", "Login");

            LoginVO login = Session["LoginInfo"] as LoginVO;
            OrderDAC db = new OrderDAC();
            var listHistory = db.GetOrderHistory(login.CustomerID);
            ViewData["Order"] = listHistory.Item1;
            ViewData["OrderDetail"] = listHistory.Item2;

            // 주문기록이 없을 경우
            if (listHistory == (null, null))
            {
                return Content("<script language='javascript' type='text/javascript'> alert('주문 내역이 없습니다.'); window.location.href='/Home/Index'</script>");
            }
            else
            {
                Session["Cart"] = null;
                LoginVO loginInfo = Session["LoginInfo"] as LoginVO;
                db.OrderView(loginInfo.CustomerID);
                return View();
            }
        }
        //[HttpPost]
        //public ActionResult OrderView()
        //{
        //    LoginVO loginInfo = Session["LoginInfo"] as LoginVO;
        //    OrderDAC db = new OrderDAC();
        //    db.OrderView(loginInfo.CustomerID);
        //    return View();
        //}
    }
}