using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class CustomerDAC
    {
        string strConn;
        public CustomerDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        // 입고처만
        public List<CustomerVO> GetCustomerType()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select CustomerID, CustomerName from TB_Customer where Category like '%입고%'";

                cmd.Connection.Open();
                List<CustomerVO> list = Helper.DataReaderMapToList<CustomerVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }


        }
    }
}