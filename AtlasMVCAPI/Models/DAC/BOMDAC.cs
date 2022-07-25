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
    public class BOMDAC
    {
        string strConn;
        public BOMDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<BOMVO> GetBOMItemList()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select BOMID, B.ItemID, ParentID, ChildID, UnitQty, 
                                           convert(nvarchar(20), B.CreateDate, 23) CreateDate, B.CreateUser, 
                                           convert(nvarchar(20), B.ModifyDate, 23) ModifyDate, B.ModifyUser, 
                                           B.StateYN, ItemName, ItemCategory, ItemSize
                                    from TB_BOM B inner join TB_Item I on B.ItemID = I.ItemID";

                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
    }
}