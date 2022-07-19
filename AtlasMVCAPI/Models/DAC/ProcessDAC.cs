using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class ProcessDAC
    {
        string strConn;
        public ProcessDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<ProcessVO> GetAllProcess()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select ProcessID, ProcessName, FailCheck, convert(varchar(10), CreateDate, 23) CreateDate, CreateUser, convert(varchar(10), ModifyDate, 23) ModifyDate, ModifyUser
                                    from TB_Process";

                cmd.Connection.Open();
                List<ProcessVO> list = Helper.DataReaderMapToList<ProcessVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }


        }
    }
}