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
                cmd.CommandText = @"select ItemID, ItemName, C.CustomerName, CurrentQty, SafeQty, WHID, ItemPrice, ItemCategory, ItemSize, ItmeImage, ItemExplain, CONVERT(varchar(30), I.CreateDate, 120) CreateDate, I.CreateUser, CONVERT(varchar(30), I.ModifyDate, 120) ModifyDate, I.ModifyUser, I.StateYN
                                    from TB_Item I left outer join TB_Customer C on I.CustomerID = C.CustomerID";

                cmd.Connection.Open();
                List<ItemVO> list = Helper.DataReaderMapToList<ItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public bool SaveItem(ItemVO process)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn),
                cmd.CommandText = @"insert into TB_Process (ProcessName, FailCheck, CreateUser)
                                values ( @ProcessName,@FailCheck, @CreateUser)";


                cmd.Parameters.AddWithValue("@ProcessName", process.ProcessName);
                cmd.Parameters.AddWithValue("@FailCheck", process.FailCheck);
                cmd.Parameters.AddWithValue("@CreateUser", "김길동");

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
    }
}