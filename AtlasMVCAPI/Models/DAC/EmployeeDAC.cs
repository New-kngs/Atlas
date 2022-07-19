using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using AtlasDTO;

namespace AtlasMVCAPI.Models
{
    public class EmployeeDAC
    {
        string strConn;
        public EmployeeDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<EmployeeVO> GetAllEmployee()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"SELECT EmpID,EmpName, EmpPwd, EmpPhone, EmpEmail, D.DeptName, E.CreateDate as CreateDate,
                                    E.CreateUser as CreateUser, E.ModifyDate as ModifyDate ,E.ModifyUser as ModifyUser,DeletedYN
                                    FROM TB_Employees E INNER JOIN TB_Derpartment D ON E.DeptID = D.DeptID ";

                cmd.Connection.Open();
                List<EmployeeVO> list = Helper.DataReaderMapToList<EmployeeVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }


        }

    }
}