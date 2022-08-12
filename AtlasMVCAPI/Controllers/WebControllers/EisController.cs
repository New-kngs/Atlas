using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-6).Day).ToString("yyyy-MM-dd");
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
            DataTable TB_Sales = ds.Tables[0];
            // DataTable을 가공한다.
            //ViewBag.Labels = "January,February,March,April,May,June,July";

            //ViewBag.Label1 = "Digital Goods";
            //ViewBag.Data1 = "[28, 48, 40, 19, 86, 27, 90]";
            
            StringBuilder sbSaleDate = new StringBuilder();
            List<string> ItemSalesName = new List<string>();
            List<string> ItemSalePrice = new List<string>();
            List<string> ItemPurchaseName = new List<string>();
            List<string> ItemPurchasePrice = new List<string>();

            // Y축 (일자별) "2020-01-02, 2020-01-03"
            for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
            {
                sbSaleDate.Append(ds.Tables[0].Columns[i].ToString().Substring(5, 5) + ",");
            }

            // 매출

            // ItemName을 List로 개별 저장
            // "란체스터베드S" "헬리온베드Q"
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ItemSalesName.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            // (데이터) "[10, 2]"
            //          "[1, 20]"
            //          "[12, 10]"
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                StringBuilder sb = new StringBuilder(); // 초기화
                for (int j=1; j<ds.Tables[0].Columns.Count; j++)
                {
                    sb.Append(ds.Tables[0].Rows[i][j] + ",");
                }
                ItemSalePrice.Add(sb.ToString());
            }
            for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
            {
                ItemSalePrice[j] = ItemSalePrice[j].TrimEnd(',');
                ItemSalePrice[j] = "[" + ItemSalePrice[j] + "]";
            }

            // 매입

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                ItemPurchaseName.Add(ds.Tables[1].Rows[i][0].ToString());
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                StringBuilder sb = new StringBuilder(); // 초기화
                for (int j = 1; j < ds.Tables[1].Columns.Count; j++)
                {
                    sb.Append(ds.Tables[1].Rows[i][j] + ",");
                }
                ItemPurchasePrice.Add(sb.ToString());
            }
            for (int j = 1; j < ds.Tables[1].Rows.Count; j++) // 제품 갯수에 맞춰 반복
            {
                ItemPurchasePrice[j] = ItemPurchasePrice[j].TrimEnd(',');
                ItemPurchasePrice[j] = "[" + ItemPurchasePrice[j] + "]";
            }

            MoneyViewModel model = new MoneyViewModel();
            model.M_Date = sbSaleDate.ToString().TrimEnd(',');
            model.ItemSalesName = ItemSalesName;
            model.ItemSalePrice = ItemSalePrice;
            model.ItemPurchaseName = ItemPurchaseName;
            model.ItemPurchasePrice = ItemPurchasePrice;
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