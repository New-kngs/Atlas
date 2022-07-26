using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;

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
                cmd.CommandText = @"select OrderID, CustomerName, OrderShip, convert(varchar(30), OrderEndDate, 120) OrderEndDate, convert(varchar(30), C.CreateDate, 120) CreateDate, C.CreateUser, convert(varchar(30), C.ModifyDate, 120) ModifyDate, C.ModifyUser 
                                    from TB_Order O inner join TB_Customer C on O.CustomerID = C.CustomerID";

                cmd.Connection.Open();
                List<OrderVO> list = Helper.DataReaderMapToList<OrderVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
    }
}