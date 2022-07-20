using AtlasDTO;
using System;
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
    }
}