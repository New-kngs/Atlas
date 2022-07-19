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
                cmd.CommandText = @"select ProcessID, ProcessName, FailCheck, convert(varchar(10), CreateDate, 120) CreateDate, 
                                    CreateUser, convert(varchar(10), ModifyDate, 120) ModifyDate, ModifyUser
                                    from TB_Process";

                cmd.Connection.Open();
                List<ProcessVO> list = Helper.DataReaderMapToList<ProcessVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public bool SaveProcess(ProcessVO process)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"insert into TB_Process (ProcessName, FailCheck, CreateUser)
                                values ( @ProcessName,@FailCheck, @CreateUser)"

            })
            {
                cmd.Parameters.AddWithValue("@ProcessName", process.ProcessName);
                cmd.Parameters.AddWithValue("@FailCheck", process.FailCheck);
                cmd.Parameters.AddWithValue("@CreateUser", "김길동");

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UpdateProcess(ProcessVO process)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Process set ProcessName = @ProcessName, FailCheck = @FailCheck, ModifyDate=@ModifyDate, ModifyUser = @ModifyUser
                                where ProcessID = @ProcessID"

            })
            {
                cmd.Parameters.AddWithValue("@ProcessName", process.ProcessName);
                cmd.Parameters.AddWithValue("@FailCheck", process.FailCheck);
                cmd.Parameters.AddWithValue("@ModifyUser", process.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreateUser", "김길동");

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

    }
}