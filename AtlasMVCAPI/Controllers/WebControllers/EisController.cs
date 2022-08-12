using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI.Controllers
{
    public class EisController : Controller
    {
        // GET: Eis
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult MoneyPage(string startDate, string endDate)
        {
            if(startDate == null)
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-7).Day).ToString("yyyy-MM-dd");
            }
            ViewBag.startDate = startDate;
            if (endDate == null)
            {
                endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("yyyy-MM-dd");
            }
            ViewBag.endDate = endDate;

            ItemDAC db = new ItemDAC();
            DataSet ds = db.GetPivotMoney(startDate, endDate);


            for(int len=0; len<ds.Tables.Count;len++)
            {
                foreach (DataRow dr in ds.Tables[len].Rows)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    { 
                        if (dr[i].ToString() == "")
                        {
                            dr[i] = 0;
                        }
                    }
                }
            }

            Session["Sales"] = ds.Tables[0];
            Session["Purchase"] = ds.Tables[1];

            return View();
        }
        public ActionResult FailPage()
        {
            FailDAC db = new FailDAC();
            List<FailVO> listFail = db.GetFailList();
            var name = from f in listFail
                       select f.FailName;
            var qty = from f in listFail
                       select f.FailQty;

            ViewBag.LabelTop4 = "[" + string.Join(",", name) + "]";
            ViewBag.DataTop4 = "[" + string.Join(",", qty) + "]";
            return View();
        }
    }
}