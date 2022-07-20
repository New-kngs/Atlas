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
                    CreateUser, convert(varchar(20), ModifyDate, 120) ModifyDate,ModifyUser, StateYN from TB_Equipment ";

                cmd.Connection.Open();
                List<EquipmentVO> list = Helper.DataReaderMapToList<EquipmentVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
    }
}