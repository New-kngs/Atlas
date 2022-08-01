using System;
using System.Collections.Generic;
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
//            select ROW_NUMBER() OVER(order by getdate()) SEQ, convert(date, O.CreateDate) DT, ItemName, SUM(Qty) Qty
//from TB_Order O
//inner join TB_OrderDetails OD on O.OrderID = OD.OrderID
//inner join TB_Item I on I.ItemID = OD.ItemID
//group by convert(date, O.CreateDate), ItemName, Qty

//select ItemName, [2022 - 07 - 19], [2022 - 07 - 27], [2022 - 07 - 28]
//from(
//    select  ROW_NUMBER() OVER(order by getdate()) SEQ, convert(date, O.CreateDate) DT, ItemName, Qty

//    from TB_Order O

//    inner
//    join TB_OrderDetails OD on O.OrderID = OD.OrderID

//inner
//    join TB_Item I on I.ItemID = OD.ItemID
//) as src
//pivot(
//    SUM(Qty)

//    for DT in ([2022 - 07 - 19], [2022 - 07 - 27], [2022 - 07 - 28])
//) p


            return View();
        }
    }
}