using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AtlasDTO;

namespace AtlasMVCAPI.Models
{
    public class DepartmentDAC
    {
        string strConn;
        public DepartmentDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<DepartmentVO> GetDepartmentAll()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select DeptID,DeptName,convert(nvarchar(20), CreateDate,120) as CreateDate,CreateUser,convert(nvarchar(20), ModifyDate,120) as ModifyDate,ModifyUser
                                    from TB_Derpartment";

                cmd.Connection.Open();
                List<DepartmentVO> list = Helper.DataReaderMapToList<DepartmentVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }

        }

    }
}