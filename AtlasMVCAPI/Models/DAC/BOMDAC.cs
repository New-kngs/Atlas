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

        //public List<BOMVO> GetBOMForwardList()
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = new SqlConnection(strConn);
        //        cmd.CommandText = @"select BOMID, B.ItemID, ParentID, ChildID, UnitQty, 
        //                                   convert(nvarchar(20), B.CreateDate, 23) CreateDate, B.CreateUser, 
        //                                   convert(nvarchar(20), B.ModifyDate, 23) ModifyDate, B.ModifyUser, 
        //                                   B.StateYN, ItemName, ItemCategory, ItemSize
        //                            from TB_BOM B inner join TB_Item I on B.ChildID = I.ItemID";

        //        cmd.Connection.Open();
        //        List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
        //        cmd.Connection.Close();

        //        return list;
        //    }
        //}

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

        public List<BOMVO> GetRegiBOMList(string category)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select I.ItemID, ItemName, ItemCategory, ItemSize, ParentID
                                    from TB_BOM B left outer join TB_Item I on B.ItemID = I.ItemID
                                    where ParentID is not null and ItemCategory = @ItemCategory
                                    group by I.ItemID, ItemName,ItemCategory, ItemSize, ParentID";
                cmd.Parameters.AddWithValue("@ItemCategory", category);

                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<BOMVO> GetBOMForwardList(string itemID)
        {
            string sql =
            @"with BOM_CTE as
            (
            select ItemID, ParentID, UnitQty, 0 levels, cast(ItemID as varchar(30)) sortOrder
            from TB_BOM where ItemID = @ItemID
            UNION ALL
            select C.ItemID, C.ParentID, C.UnitQty, (P.levels +1) levels, cast(P.sortOrder + '>' + C.ItemID as varchar(30)) sortOrder
            from TB_BOM C inner join BOM_CTE P on C.ParentID = P.ItemID
            )
            
            select distinct case when B.levels = 0 then '' else REPLICATE('   ', B.levels) + 'L ' end + T.ItemName ItemName, ItemCategory, ItemSize,
                   B.ItemID, T.ItemName, B.UnitQty, LEVELS, sortOrder
            from BOM_CTE B join TB_Item T on B.ItemID = T.ItemID
            order by sortOrder";

            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }
        }

        public List<BOMVO> GetBOMRewardList(string itemID)
        {
            string sql =
            @"with BOM_CTE as
                (
                select ItemID, ParentID, UnitQty, 0 levels, cast(ItemID as varchar(30)) sortOrder
                from TB_BOM where ItemID = @ItemID
                UNION ALL
                select C.ItemID, C.ParentID, C.UnitQty, (P.levels +1) levels, cast(P.sortOrder + '>' + C.ItemID as varchar(30)) sortOrder
                from TB_BOM C inner join BOM_CTE P on C.ItemID = P.ParentID
                )            
            select distinct case when B.levels = 0 then '' else REPLICATE('   ', B.levels) + 'L ' end + T.ItemName ItemName, 
                   B.ItemID, T.ItemName, B.UnitQty, LEVELS, sortOrder
            from BOM_CTE B join TB_Item T on B.ItemID = T.ItemID
            order by sortOrder";

            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }
        }

        public bool SaveBOM(List<BOMVO> list)
        {
           // @ItemID1, @ParentID1, @ChildID1, @UnitQty1, @CreateDate1, @CreateUser1,
           // @ItemID2, @ParentID2, @ChildID2, @UnitQty2, @CreateDate2, @CreateUser2
            foreach (BOMVO item in list)
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = new SqlConnection(strConn),
                    CommandText = "SP_CreateBOM",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ItemID1", item.ItemID);
                    cmd.Parameters.AddWithValue("@ParentID1", item.ParentID);
                    cmd.Parameters.AddWithValue("@ChildID1", item.ChildID);
                    cmd.Parameters.AddWithValue("@UnitQty1", item.UnitQty);
                    cmd.Parameters.AddWithValue("@CreateDate1", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreateUser1", item.CreateUser);
                    cmd.Parameters.AddWithValue("@ItemID2", item.ItemID);
                    cmd.Parameters.AddWithValue("@ParentID2", item.ParentID);
                    cmd.Parameters.AddWithValue("@ChildID2", item.ChildID);
                    cmd.Parameters.AddWithValue("@UnitQty2", item.UnitQty);
                    cmd.Parameters.AddWithValue("@CreateDate2", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreateUser2", item.CreateUser);
                    cmd.Connection.Open();
                    int iRowAffect = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    return (iRowAffect > 0);
                }
            }
        }
    }
}