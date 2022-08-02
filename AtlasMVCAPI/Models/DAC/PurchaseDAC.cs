using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class PurchaseDAC
    {
        string strConn;
        public PurchaseDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<PurchaseVO> GetAllPurchase()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select PurchaseID, CustomerName, convert(varchar(20), P.PurchaseEndDate, 120) PurchaseEndDate, WHName, InState, convert(varchar(20), P.CreateDate, 120) CreateDate, P.CreateUser, convert(varchar(20), P.ModifyDate, 120) ModifyDate, P.ModifyUser
                                    from TB_Purchase P Inner join TB_Customer C on P.CustomerID = C.CustomerID
				                                       left outer join TB_Warehouse W on P.WHID = W.WHID";

                cmd.Connection.Open();
                List<PurchaseVO> list = Helper.DataReaderMapToList<PurchaseVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }



        public List<PurchaseVO> GetSearchPurchase(string from, string to)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select PurchaseID, CustomerName, InState, convert(varchar(30), PurchaseEndDate, 120) PurchaseEndDate, WHName, convert(varchar(30), P.CreateDate, 120) CreateDate, P.CreateUser, convert(varchar(30), P.ModifyDate, 120) ModifyDate, P.ModifyUser
                                    from TB_Purchase P inner join TB_Customer C on P.CustomerID = C.CustomerID
				                                       left outer join TB_Warehouse W on P.WHID = W.WHID
                                    where P.CreateDate Between @from and @to";

                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                cmd.Connection.Open();
                List<PurchaseVO> list = Helper.DataReaderMapToList<PurchaseVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }


        public PurchaseVO GeTPurchaseById(string id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select PurchaseID, CustomerName, InState, convert(varchar(30), PurchaseEndDate, 120) PurchaseEndDate, WHName, convert(varchar(30), P.CreateDate, 120) CreateDate, P.CreateUser, convert(varchar(30), P.ModifyDate, 120) ModifyDate, P.ModifyUser
                                    from TB_Purchase P inner join TB_Customer C on P.CustomerID = C.CustomerID
                                                       left outer join TB_Warehouse W on P.WHID = W.WHID
                                    where PurchaseID = @id";

                cmd.Parameters.AddWithValue("@id", id);

                cmd.Connection.Open();
                List<PurchaseVO> list = Helper.DataReaderMapToList<PurchaseVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }

        //public List<OrderDetailVO> GetAllOrderDetail()
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = new SqlConnection(strConn);
        //        cmd.CommandText = @"select OrderID, OD.ItemID, ItemName, Qty
        //                            from TB_OrderDetails  OD inner join TB_Item I on OD.ItemID = I.ItemID";

        //        cmd.Connection.Open();
        //        List<OrderDetailVO> list = Helper.DataReaderMapToList<OrderDetailVO>(cmd.ExecuteReader());
        //        cmd.Connection.Close();

        //        return list;
        //    }
        //}
    }
}