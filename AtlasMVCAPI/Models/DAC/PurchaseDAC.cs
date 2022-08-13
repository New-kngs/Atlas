using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data;
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

        // 발주ID
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
       
        public bool SavePurchasex(PurchaseVO pur, List<PurchaseDetailsVO> purDetail)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();

            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "SP_CreatePurchase";
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", pur.CustomerID);
                    cmd.Parameters.AddWithValue("@CreateUser", pur.CreateUser);
                    //cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);

                    string PurchaseId = cmd.ExecuteScalar().ToString();

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"insert into TB_PurchaseDetails(PurchaseID, ItemID, Qty) values(@PurchaseID, @ItemID, @Qty)";

                    cmd.Parameters.AddWithValue("@PurchaseID", PurchaseId);
                    cmd.Parameters.Add("@ItemID", SqlDbType.NVarChar, 10);
                    cmd.Parameters.Add("@Qty", SqlDbType.Int);

                    int iRowAffect = 0;
                    foreach (PurchaseDetailsVO item in purDetail)
                    {
                        cmd.Parameters[@"ItemID"].Value = item.ItemID;
                        cmd.Parameters[@"Qty"].Value = item.Qty;

                        iRowAffect += cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return (iRowAffect > 0);
                }
            }
            catch (Exception err)
            {
                string sss = err.Message;
                trans.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool SavePurchase(string CustomerID, string CreateUser, string sbItemID, string sbQty)
        {
            using (SqlCommand cmd = new SqlCommand())
            {            
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "SP_CreatePurchase";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CreateUser", CreateUser);
                cmd.Parameters.AddWithValue("@sbItemID", sbItemID);
                cmd.Parameters.AddWithValue("@sbQty", sbQty);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }


        public List<PurchaseDetailsVO> GetAllPurchaseDetail()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select PurchaseID, PD.ItemID, ItemName, ItemSize, Qty, ItemPrice
                                    from TB_PurchaseDetails PD inner join TB_Item I on PD.ItemID = I.ItemID";

                cmd.Connection.Open();
                List<PurchaseDetailsVO> list = Helper.DataReaderMapToList<PurchaseDetailsVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }


    }
}