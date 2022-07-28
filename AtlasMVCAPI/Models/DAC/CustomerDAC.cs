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

        public List<CustomerVO> GetAllCustomer()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select CustomerID, CustomerPwd, CustomerName, Category, Email, Address, Phone, EmpID, CONVERT(varchar(30), CreateDate, 120) CreateDate, CreateUser, CONVERT(varchar(30), ModifyDate, 120) ModifyDate, ModifyUser, StateYN 
                                    from TB_Customer";

                cmd.Connection.Open();
                List<CustomerVO> list = Helper.DataReaderMapToList<CustomerVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }
    }
}