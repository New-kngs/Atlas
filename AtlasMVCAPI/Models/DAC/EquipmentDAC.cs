using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class EquipmentDAC
    {
        string strConn;
        public EquipmentDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<EquipmentVO> GetAllEquipment()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select EquipID, EquipName, EquipCategory, convert(varchar(20), CreateDate, 120) CreateDate, 
                    CreateUser, convert(varchar(20), ModifyDate, 120) ModifyDate, ModifyUser, StateYN from TB_Equipment";

                cmd.Connection.Open();
                List<EquipmentVO> list = Helper.DataReaderMapToList<EquipmentVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public bool SaveEquip(EquipmentVO equip)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"insert into TB_Equipment (EquipName, EquipCategory, CreateUser)
                                 values(@EquipName, @EquipCategory, @CreateUser)"
            })
            {
                cmd.Parameters.AddWithValue("@EquipName", equip.EquipName);
                cmd.Parameters.AddWithValue("@EquipCategory", equip.EquipCategory);
                cmd.Parameters.AddWithValue("@CreateUser", equip.CreateUser);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
        public bool UpdateEquip(EquipmentVO equip)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Equipment set EquipName = @EquipName, EquipCategory = @EquipCategory, ModifyDate=@ModifyDate, ModifyUser = @ModifyUser
                                where EquipID = @EquipID"

            })
            {
                cmd.Parameters.AddWithValue("@EquipName", equip.EquipName);
                cmd.Parameters.AddWithValue("@EquipCategory", equip.EquipCategory);
                cmd.Parameters.AddWithValue("@EquipID", equip.EquipID);
                cmd.Parameters.AddWithValue("@ModifyUser", equip.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool DeleteEquip(EquipmentVO equip)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Equipment set StateYN = 'N', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where EquipID = @EquipID"

            })
            {
                cmd.Parameters.AddWithValue("@EquipID", equip.EquipID);
                cmd.Parameters.AddWithValue("@ModifyUser", equip.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UsingEquip(EquipmentVO equip)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Equipment set StateYN = 'Y', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where EquipID = @EquipID"

            })
            {
                cmd.Parameters.AddWithValue("@EquipID", equip.EquipID);
                cmd.Parameters.AddWithValue("@ModifyUser", equip.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
    }
}