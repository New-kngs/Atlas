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
                cmd.CommandText = @"SELECT Eid,EmpID,EmpName, EmpPwd, EmpPhone, EmpEmail, D.DeptName, convert(nvarchar(20),E.CreateDate,120) as CreateDate,
                                    E.CreateUser as CreateUser,  convert(nvarchar(20), E.ModifyDate,120) as ModifyDate ,E.ModifyUser as ModifyUser,convert(nvarchar(5), StateYN) as StateYN
                                    FROM TB_Employees E INNER JOIN TB_Department D ON E.DeptID = D.DeptID";

                cmd.Connection.Open();
                List<EmployeeVO> list = Helper.DataReaderMapToList<EmployeeVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }


        }


        public List<ComboItemVO> GetDomainCategory()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select Code, Category, CodeName from TB_CommonCode where Category = 'Domain'
                                    order by Code";

                cmd.Connection.Open();
                List<ComboItemVO> list = Helper.DataReaderMapToList<ComboItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public List<EmployeeVO> GetSalesEmplist()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @" select EmpID,EmpName from TB_Employees
                                     where DeptId = 9";

                cmd.Connection.Open();
                List<EmployeeVO> list = Helper.DataReaderMapToList<EmployeeVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }


        }

        public bool SaveEmployee(EmployeeVO emp)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"insert into TB_Employees (EmpID, EmpName, EmpPwd , EmpPhone , EmpEmail ,DeptID , CreateDate , CreateUser)
                                values (@EmpID , @EmpName ,@EmpPwd, @EmpPhone, @EmpEmail, @DeptID, @CreateDate , @CreateUser)"

            })
            {
                cmd.Parameters.AddWithValue("@EmpID", emp.EmpID);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@EmpPwd", emp.EmpPwd);
                cmd.Parameters.AddWithValue("@EmpPhone", emp.EmpPhone);
                cmd.Parameters.AddWithValue("@EmpEmail", emp.EmpEmail);
                cmd.Parameters.AddWithValue("@DeptID", emp.DeptName);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreateUser", emp.CreateUser);


                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }



        public bool UpdateEmployee(EmployeeVO emp)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"Update TB_Employees
                                   set EmpID = @EmpID, EmpName = @EmpName, EmpPwd = @EmpPwd, EmpPhone = @EmpPhone, DeptID = @DeptID,
	                                   ModifyDate = @ModifyDate, ModifyUser = @ModifyUser
                                 where Eid = @Eid"

            })
            {
                cmd.Parameters.AddWithValue("@Eid", emp.Eid);
                cmd.Parameters.AddWithValue("@EmpID", emp.EmpID);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@EmpPwd", emp.EmpPwd);
                cmd.Parameters.AddWithValue("@EmpPhone", emp.EmpPhone);
                cmd.Parameters.AddWithValue("@EmpEmail", emp.EmpEmail);
                cmd.Parameters.AddWithValue("@DeptID", emp.DeptName);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifyUser", emp.ModifyUser);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }

        }


        public bool DeleteEmployee(EmployeeVO emp)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"Delete from TB_Employees
                                where Eid = @Eid"

            })
            {
                cmd.Parameters.AddWithValue("@Eid", emp.Eid);
               

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }

        }

    }
}