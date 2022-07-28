using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AtlasMVCAPI.Models
{
    public class OrderDAC
    {
        string strConn;

        public OrderDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<OrderVO> GetAllOrder()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select OrderID, CustomerName, OrderShip, convert(varchar(30), OrderEndDate, 120) OrderEndDate, convert(varchar(30), O.CreateDate, 120) CreateDate, O.CreateUser, convert(varchar(30), O.ModifyDate, 120) ModifyDate, O.ModifyUser 
                                    from TB_Order O inner join TB_Customer C on O.CustomerID = C.CustomerID";

                cmd.Connection.Open();
                List<OrderVO> list = Helper.DataReaderMapToList<OrderVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<OrderVO> GetSearchOrder(string from, string to)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select OrderID, CustomerName, OrderShip, convert(varchar(30), OrderEndDate, 120) OrderEndDate, convert(varchar(30), O.CreateDate, 120) CreateDate, O.CreateUser, convert(varchar(30), O.ModifyDate, 120) ModifyDate, O.ModifyUser 
                                    from TB_Order O inner join TB_Customer C on O.CustomerID = C.CustomerID
                                    where O.CreateDate Between @from and @to";

                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                cmd.Connection.Open();
                List<OrderVO> list = Helper.DataReaderMapToList<OrderVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }


        public OrderVO GeTOrderById(string id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select OrderID, CustomerName, OrderShip, convert(varchar(30), OrderEndDate, 120) OrderEndDate, convert(varchar(30), O.CreateDate, 120) CreateDate, C.CreateUser, convert(varchar(30), C.ModifyDate, 120) ModifyDate, C.ModifyUser 
                                    from TB_Order O inner join TB_Customer C on O.CustomerID = C.CustomerID
                                    where OrderID = @id";

                cmd.Parameters.AddWithValue("@id", id);

                cmd.Connection.Open();
                List<OrderVO> list = Helper.DataReaderMapToList<OrderVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }


        /// <summary>
        /// 주문명세를 생성한다 (작성자-지현)
        /// </summary>
        public void CreateOrder(string CustomerID, string CreateUser, string sbItemID, string sbQty)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // OrderID을 1 증가 후, OrderID를 다시 가져온다.
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "SP_CreateOrder";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@CreateUser", CreateUser);
                cmd.Parameters.AddWithValue("@sbItemID", sbItemID);
                cmd.Parameters.AddWithValue("@sbQty", sbQty);

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
    }
}