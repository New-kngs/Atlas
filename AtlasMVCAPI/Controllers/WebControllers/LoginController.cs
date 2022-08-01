using AtlasDTO;
using AtlasMVCAPI.Models.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers
{
    public class LoginController : Controller
    {
        // 잠겨진 로그인 창
        public ActionResult Lock()
        {
            return View();
        }

        /// <summary>
        /// 거래처, 임원 둘 중 누가 로그인 하냐에 따라서 Layout이 달라짐
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ButtonClick(string LoginID, string LoginPWD)
        {
            LoginDAC db = new LoginDAC();
            LoginVO user = db.LoginCheck(LoginID, LoginPWD);
            if (user == null)
            {
                // ID와 PWD를 잘못 입력하셨습니다. 재시도 해주세요.
                // return RedirectToAction("Lock", "Login");
                return Content("<script language='javascript' type='text/javascript'> alert('ID와 PWD를 잘못 입력하셨습니다. 재시도 해주세요.'); window.location.href='/Login/Lock'</script>");
            }

            else if (user.CustomerID != null) // 사용자가 거래처라면
            {
                Session["LoginInfo"] = user;

                return RedirectToAction("Index", "Home");
            }
            else // if (user.EmpID != null) // 사용자가 임원이라면
            {
                Session["LoginInfo"] = user;

                return RedirectToAction("HomePage", "Eis");
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["LoginInfo"] = null;

            Session.Clear();
            return RedirectToAction("Lock");
        }
    }
}