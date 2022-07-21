using AtlasDTO;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class WareHouseDAC
    {
        //WHID, WHName, ItemCategory, CreateDate, CreateUser, ModifyDate, ModifyUser, StateYN
        string strConn;
        public WareHouseDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<WareHouseVO> GetAllWareHouse()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select WHID, WHName, ItemCategory, convert(varchar(20), CreateDate, 120) CreateDate, CreateUser, convert(varchar(20), ModifyDate, 120) ModifyDate, ModifyUser, StateYN
                                    from TB_Warehouse";

                cmd.Connection.Open();
                List<WareHouseVO> list = Helper.DataReaderMapToList<WareHouseVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public List<ItemVO> GetWareHouseInfo(string whid)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select ItemID, ItemName, CurrentQty, WHID, ItemCategory, ItemSize
                                    from [dbo].[TB_Item]
                                    where WHID = @WHID";
                cmd.Parameters.AddWithValue("@WHID", whid);

                cmd.Connection.Open();
                List<ItemVO> list = Helper.DataReaderMapToList<ItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public bool DeleteWareHouse(WareHouseVO wareHouse)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Warehouse 
                                set StateYN = 'N', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where WHID = @WHID"
            })
            {
                cmd.Parameters.AddWithValue("@WHID", wareHouse.WHID);
                cmd.Parameters.AddWithValue("@ModifyUser", "김길동");
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UsingWareHouse(WareHouseVO wareHouse)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Warehouse set StateYN = 'Y', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where WHID = @WHID"
            })
            {
                cmd.Parameters.AddWithValue("@WHID", wareHouse.WHID);
                cmd.Parameters.AddWithValue("@ModifyUser", "김길동");
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UpdateWareHouse(WareHouseVO wareHouse)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Warehouse set WHName=@WHName, ItemCategory=@ItemCategory, ModifyDate=@ModifyDate, ModifyUser = @ModifyUser                  where WHID = @WHID"
            })
            {
                cmd.Parameters.AddWithValue("@WHID", wareHouse.WHID);
                cmd.Parameters.AddWithValue("@WHName", wareHouse.WHName);
                cmd.Parameters.AddWithValue("@ItemCategory", wareHouse.ItemCategory);
                cmd.Parameters.AddWithValue("@ModifyUser", "김길동");
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool SaveWareHouse(WareHouseVO wareHouse)
        {
            //@WHName, @ItemCategory, @CreateDate, @CreateUser
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = "SP_CreateWareHouse",
                CommandType = CommandType.StoredProcedure
            })
            {
                cmd.Parameters.AddWithValue("@WHName", wareHouse.WHName);
                cmd.Parameters.AddWithValue("@ItemCategory", wareHouse.ItemCategory);
                cmd.Parameters.AddWithValue("@CreateUser", "김길동");
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
    }
}