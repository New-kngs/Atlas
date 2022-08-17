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
                cmd.CommandText = @"select PurchaseID, CustomerName, InState, convert(varchar(30), PurchaseEndDate, 120) PurchaseEndDate, convert(varchar(30), P.CreateDate, 120) CreateDate, P.CreateUser, convert(varchar(30), P.ModifyDate, 120) ModifyDate, P.ModifyUser
                                    from TB_Purchase P inner join TB_Customer C on P.CustomerID = C.CustomerID
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

        public bool UpdatePurchase(string purId, string modifyuser, string sbItemID, string sbQty)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "SP_UpdatePurchase";
                cmd.CommandType = CommandType.StoredProcedure;

                // input
                cmd.Parameters.AddWithValue("@PurchaseID", purId);
                cmd.Parameters.AddWithValue("@ModifyUser", modifyuser);
                cmd.Parameters.AddWithValue("@sbItemID", sbItemID);
                cmd.Parameters.AddWithValue("@sbQty", sbQty);
                //cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);

                // output
                //cmd.Parameters.Add(new SqlParameter("@PO_CD", SqlDbType.Int)).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(new SqlParameter("@PO_MSG", SqlDbType.NVarChar, 1000)).Direction = ParameterDirection.Output;
                               
               
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }

        }

        public bool UpdatePurStateItemQty(PurchaseVO pur)
        {
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "SP_UpdatePurStateItemQty";
                cmd.CommandType = CommandType.StoredProcedure;

                // input
                cmd.Parameters.AddWithValue("@PurchaseID", pur.PurchaseID);               

                // output
                //cmd.Parameters.Add(new SqlParameter("@PO_CD", SqlDbType.Int)).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(new SqlParameter("@PO_MSG", SqlDbType.NVarChar, 1000)).Direction = ParameterDirection.Output;


                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }

        }

        public bool DeletePurchase(PurchaseVO purId)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "SP_DeletePurchase";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@PurchaseID", purId.PurchaseID);

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
                cmd.CommandText = @"select PD.PurchaseID, PD.ItemID, ItemName, ItemSize, Qty, ItemPrice , InState
                                    from TB_PurchaseDetails PD inner join TB_Item I on PD.ItemID = I.ItemID
							                                   inner join TB_Purchase P on PD.PurchaseID = P.PurchaseID";

                cmd.Connection.Open();
                List<PurchaseDetailsVO> list = Helper.DataReaderMapToList<PurchaseDetailsVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }



        public List<RptPurchaseVO> GetRptPurchase()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select P.PurchaseID, CustomerName, PD.ItemID, ItemName, ItemSize, Qty, convert(varchar(20), P.CreateDate, 120) CreateDate ,convert(varchar(20), P.PurchaseEndDate, 120) PurchaseEndDate, InState, Address, Email, Phone, EmpName, ItemPrice
                                    from TB_PurchaseDetails PD inner join TB_Purchase P on PD.PurchaseID = P.PurchaseID
						                                       inner join TB_Item I on PD.ItemID =I.ItemID
						                                       inner join TB_Customer C on I.CustomerID = C.CustomerID
						                                       inner join TB_Employees E on C.EmpID = E.EmpID";
                                    

                cmd.Connection.Open();
                List<RptPurchaseVO> list = Helper.DataReaderMapToList<RptPurchaseVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }


    }
}