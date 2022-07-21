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
                cmd.CommandText = @"select ProcessID, ProcessName, FailCheck, convert(varchar(20), CreateDate, 120) CreateDate, 
                    CreateUser, convert(varchar(20), ModifyDate, 120) ModifyDate,ModifyUser, StateYN from TB_Process ";

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
                cmd.Parameters.AddWithValue("@CreateUser", process.CreateUser);

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
                cmd.Parameters.AddWithValue("@ProcessID", process.ProcessID);
                cmd.Parameters.AddWithValue("@ProcessName", process.ProcessName);
                cmd.Parameters.AddWithValue("@FailCheck", process.FailCheck);
                cmd.Parameters.AddWithValue("@ModifyUser", process.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool DeleteProcess(ProcessVO process)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),

                CommandText = @"update TB_Process set StateYN = 'N', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where ProcessID = @ProcessID"

            })
            {
                cmd.Parameters.AddWithValue("@ProcessID", process.ProcessID);
                cmd.Parameters.AddWithValue("@ModifyUser", process.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UsingProcess(ProcessVO process)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Process set StateYN = 'Y', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where ProcessID = @ProcessID"

            })
            {
                cmd.Parameters.AddWithValue("@ProcessID", process.ProcessID);
                cmd.Parameters.AddWithValue("@ModifyUser", process.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public List<ComboItemVO> GetEquipName()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @" select CONVERT(varchar(10), EquipID) Code, EquipName CodeName, '설비' Category from TB_Equipment";

                cmd.Connection.Open();
                List<ComboItemVO> list = Helper.DataReaderMapToList<ComboItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public bool SaveProcessEquip(List<EquipDetailsVO> equip)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = new SqlConnection(strConn),
                    CommandText = @"insert into TB_EquipmentDetails(ProcessID, EquipID, CreateDate, CreateUser)
                                values(@ProcessID, @EquipID, @CreateDate, @CreateUser)"

                })
                {
                    cmd.Parameters.Add("@ProcessID", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@EquipID", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@CreateUser", System.Data.SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@CreateDate", System.Data.SqlDbType.DateTime);

                    cmd.Connection.Open();
                    foreach (EquipDetailsVO item in equip)
                    {
                        cmd.Parameters["@ProcessID"].Value = item.ProcessID;
                        cmd.Parameters["@EquipID"].Value = item.EquipID;
                        cmd.Parameters["@CreateDate"].Value = DateTime.Now;
                        cmd.Parameters["@CreateUser"].Value = item.CreateUser;
                        cmd.ExecuteNonQuery();
                    }
                    cmd.Connection.Close();
                    return true;
                }
            }catch(Exception err)
            {
                return false;
            }
            
        }

        public List<EquipDetailsVO> GetProcessEquip()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select ProcessID, e.EquipID, EquipName
                                    from TB_EquipmentDetails ed join TB_Equipment e on ed.EquipID = e.EquipID";

                cmd.Connection.Open();
                List<EquipDetailsVO> list = Helper.DataReaderMapToList<EquipDetailsVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
    }
}