using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class EmplogDAC
    {
        string strConn;
        public EmplogDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<EmplogVO> GetEmplog()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"Select L.EmpID as EmpID , E.EmpName as EmpName, D.DeptName as DeptName ,LogText,  convert(nvarchar(20), LogDate,120) as LogDate
                                    from TB_LogRecord as L 
                                    inner join TB_Employees as E on L.EmpID = E.EmpID
                                    inner join TB_Department as D on E.DeptID = D.DeptID";

                cmd.Connection.Open();
                List<EmplogVO> list = Helper.DataReaderMapToList<EmplogVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

    }
}