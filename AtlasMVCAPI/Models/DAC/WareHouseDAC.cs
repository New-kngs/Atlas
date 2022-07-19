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
        //WHID, WHName, ItemCategory, CreateDate, CreateUser, ModifyDate, ModifyUser, DeletedYN
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
                cmd.CommandText = @"select WHID, WHName, ItemCategory, CreateDate, CreateUser, ModifyDate, ModifyUser, DeletedYN
                                    from TB_Warehouse";

                cmd.Connection.Open();
                List<WareHouseVO> list = Helper.DataReaderMapToList<WareHouseVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }


        }
    }
}