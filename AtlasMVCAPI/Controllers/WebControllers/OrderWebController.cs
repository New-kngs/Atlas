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
            List<OrderVO> model = db.OrderListView(login.CustomerID);

            // 주문기록이 없을 경우
            if (model == null)
            {
                return Content("<script language='javascript' type='text/javascript'> alert('주문 내역이 없습니다.'); window.location.href='/Home/Index'</script>");
            }
            else
            {
                return View(model);
            }
        }
       
        public ActionResult PurchaseOrder(string rdoCheck)
        {
            OrderDAC db = new OrderDAC();
            List<OrderDetailLongVO> model = db.GetOrderDetails(rdoCheck);
            ViewData["OrderID"] = rdoCheck;

            string endDate = db.GetOrderEndDate(rdoCheck);
            ViewData["EndDate"] = endDate.Substring(0, 10);

            return View(model);
        }
    }
}