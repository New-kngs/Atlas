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
                cmd.CommandText = @"SELECT EmpID,EmpName, EmpPwd, EmpPhone, EmpEmail, D.DeptName, convert(nvarchar(20),E.CreateDate,120) as CreateDate,
                                    E.CreateUser as CreateUser,  convert(nvarchar(20), E.ModifyDate,120) as ModifyDate ,E.ModifyUser as ModifyUser,convert(nvarchar(5), StateYN) as StateYN
                                    FROM TB_Employees E INNER JOIN TB_Derpartment D ON E.DeptID = D.DeptID";

                cmd.Connection.Open();
                List<EmployeeVO> list = Helper.DataReaderMapToList<EmployeeVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }


        }

    }
}