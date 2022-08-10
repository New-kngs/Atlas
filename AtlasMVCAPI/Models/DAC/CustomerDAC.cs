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

        public List<CustomerVO> GetCustomerlist()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select CustomerID, CustomerPwd, CustomerName, Category, Email, Address, Phone, C.EmpID as EmpID, E.EmpName as EmpName, CONVERT(varchar(30), C.CreateDate, 120) CreateDate, C.CreateUser as CreateUser, CONVERT(varchar(30), C.ModifyDate, 120) ModifyDate, C.ModifyUser as ModifyUser, C.StateYN as StateYN
                                    from TB_Customer C join TB_Employees E on C.EmpID = E.EmpID";

                cmd.Connection.Open();
                List<CustomerVO> list = Helper.DataReaderMapToList<CustomerVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public bool SaveCustomer(CustomerVO vo)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"insert into TB_Customer (CustomerID, CustomerPwd, CustomerName , Category , Email ,Address , Phone , EmpID , CreateDate ,CreateUser)
                                values (@CustomerID, @CustomerPwd ,@CustomerName, @Category, @Email, @Address, @Phone, @EmpID, @CreateDate, @CreateUser)"

            })
            {
                cmd.Parameters.AddWithValue("@CustomerID", vo.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerPwd", vo.CustomerPwd);
                cmd.Parameters.AddWithValue("@CustomerName", vo.CustomerName);
                cmd.Parameters.AddWithValue("@Category", vo.Category);
                cmd.Parameters.AddWithValue("@Email", vo.Email);
                cmd.Parameters.AddWithValue("@Address", vo.Address);
                cmd.Parameters.AddWithValue("@Phone", vo.Phone);
                cmd.Parameters.AddWithValue("@EmpID", vo.EmpID);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreateUser", vo.CreateUser);


                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UpdateCustomer(CustomerVO vo)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"Update TB_Customer
                                   set CustomerName =@CustomerName, CustomerPwd = @CustomerPwd, Category = @Category, Email = @Email, Address = @Address, EmpID = @EmpID,
	                                   ModifyDate = @ModifyDate, ModifyUser = @ModifyUser
                                 where CustomerID = @CustomerID"

            })
            {
                cmd.Parameters.AddWithValue("@CustomerID", vo.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerPwd", vo.CustomerPwd);
                cmd.Parameters.AddWithValue("@CustomerName", vo.CustomerName);
                cmd.Parameters.AddWithValue("@Category", vo.Category);
                cmd.Parameters.AddWithValue("@Email", vo.Email);
                cmd.Parameters.AddWithValue("@Address", vo.Address);
                cmd.Parameters.AddWithValue("@Phone", vo.Phone);
                cmd.Parameters.AddWithValue("@EmpID", vo.EmpID);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifyUser", vo.ModifyUser);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }

        }

        public bool DeleteCustomer(CustomerVO vo)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"Delete from TB_Customer
                                where CustomerID = @CustomerID"

            })
            {
                cmd.Parameters.AddWithValue("@CustomerID", vo.CustomerID);


                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }

        }

    }
}