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

        public List<BOMVO> GetBOMForwardList()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select BOMID, B.ItemID, ParentID, ChildID, UnitQty, 
                                           convert(nvarchar(20), B.CreateDate, 23) CreateDate, B.CreateUser, 
                                           convert(nvarchar(20), B.ModifyDate, 23) ModifyDate, B.ModifyUser, 
                                           B.StateYN, ItemName, ItemCategory, ItemSize
                                    from TB_BOM B inner join TB_Item I on B.ChildID = I.ItemID";

                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<BOMVO> GetUnregiBOMList()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select I.ItemID, ItemName, ItemCategory, ItemSize, ParentID, ChildID, UnitQty, 
                                           B.CreateDate, B.CreateUser, B.ModifyDate, B.ModifyUser, B.StateYN
                                    from TB_Item I left outer join  TB_BOM B on I.ItemID = B.ItemID
                                    where ParentID is null";

                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<BOMVO> GetRegiBOMList()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select I.ItemID, ItemName, ItemCategory, ItemSize, ParentID, ChildID, UnitQty, 
                                           B.CreateDate, B.CreateUser, CONVERT(varchar(50), B.ModifyDate , 23) ModifyDate, B.ModifyUser, B.StateYN
                                    from TB_Item I left outer join  TB_BOM B on I.ItemID = B.ItemID
                                    where ChildID is not null";

                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
    }
}