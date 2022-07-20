using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AtlasDTO;
using System.Data.SqlClient;

namespace AtlasMVCAPI.Models
{
    public class ItemDAC
    {
        string strConn;
        public ItemDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<ItemVO> GetAllItem()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select ItemID, ItemName, CustomerID, CurrentQty, SafeQty, WHID, ItemPrice, ItemCategory, ItemSize, ItmeImage, ItemExplain, CONVERT(varchar(30), CreateDate, 120) CreateDate, CreateUser, CONVERT(varchar(30), ModifyDate, 120) ModifyDate, ModifyUser, StateYN
                                    from TB_Item";

                cmd.Connection.Open();
                List<ItemVO> list = Helper.DataReaderMapToList<ItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
    }
}