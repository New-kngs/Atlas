using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AtlasDTO;
using System.Data.SqlClient;
using System.Data;

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
                cmd.CommandText = @"select ItemID, ItemName, C.CustomerName, CurrentQty, SafeQty, W.WHName, ItemPrice, I.ItemCategory, ItemSize, ItemImage, ItemExplain, CONVERT(varchar(30), I.CreateDate, 120) CreateDate, I.CreateUser, CONVERT(varchar(30), I.ModifyDate, 120) ModifyDate, I.ModifyUser, I.StateYN 
                                    from TB_Item I left outer join TB_Customer C on I.CustomerID = C.CustomerID
				                                   inner join TB_Warehouse W on I.WHID = W.WHID";

                cmd.Connection.Open();
                List<ItemVO> list = Helper.DataReaderMapToList<ItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        public List<ComboItemVO> GetAllItemCategory()
        {           
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "select Code, Category, CodeName from TB_CommonCode where Category in ('완제품','반제품','자재')";

                cmd.Connection.Open();
                List<ComboItemVO> list = Helper.DataReaderMapToList<ComboItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();                               

                if (list != null && list.Count > 0)
                    return list;
                else
                    return null;
            }
        }

        
        public ItemVO GeTItemById(string id)
        {                         
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select ItemID, ItemName, C.CustomerName, CurrentQty, SafeQty, W.WHName, ItemPrice, I.ItemCategory, ItemSize, ItemImage, ItemExplain, CONVERT(varchar(30), I.CreateDate, 120) CreateDate, I.CreateUser, CONVERT(varchar(30), I.ModifyDate, 120) ModifyDate, I.ModifyUser, I.StateYN 
                                    from TB_Item I left outer join TB_Customer C on I.CustomerID = C.CustomerID
			                                        inner join TB_Warehouse W on I.WHID = W.WHID
                                    where ItemID = @id";

                cmd.Parameters.AddWithValue("@id", id);

                cmd.Connection.Open();
                List<ItemVO> list = Helper.DataReaderMapToList<ItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }            
        }

        // @ItemName, @CustomerID, @CurrentQty, @SafeQty, @WHID, @ItemPrice, @ItemCategory, @ItemSize, @ItemImage, @ItemExplain, @CreateDate, @CreateUser
        public bool SaveItem(ItemVO item)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "SP_SaveItem";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@p_ItemCode", item.p_ItemCode);
                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                cmd.Parameters.AddWithValue("@CustomerID", item.CustomerID);
                cmd.Parameters.AddWithValue("@CurrentQty", item.CurrentQty);
                cmd.Parameters.AddWithValue("@SafeQty", item.SafeQty);
                cmd.Parameters.AddWithValue("@WHID", item.WHID);
                cmd.Parameters.AddWithValue("@ItemPrice", item.ItemPrice);
                cmd.Parameters.AddWithValue("@ItemCategory", item.ItemCategory);
                cmd.Parameters.AddWithValue("@ItemSize", item.ItemSize);
                cmd.Parameters.AddWithValue("@ItemImage", item.ItemImage);
                cmd.Parameters.AddWithValue("@ItemExplain", item.ItemExplain);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreateUser", item.CreateUser);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
        
        public bool UpdateItem(ItemVO item)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"update TB_Item set CurrentQty = @CurrentQty, SafeQty = @SafeQty, ItemPrice = @ItemPrice, ItemImage = @ItemImage, ItemExplain = @ItemExplain, ModifyDate = @ModifyDate, ModifyUser = @ModifyUser
                                                   where ItemID = @ItemID";

                cmd.Parameters.AddWithValue("@ItemID", item.ItemID);
                cmd.Parameters.AddWithValue("@CurrentQty", item.ItemName);
                cmd.Parameters.AddWithValue("@SafeQty", item.ItemName);
                cmd.Parameters.AddWithValue("@ItemPrice", item.ItemName);
                cmd.Parameters.AddWithValue("@ItemImage", item.ItemName);
                cmd.Parameters.AddWithValue("@ItemExplain", item.ItemName);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifyUser", item.ItemName);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
          
        }



        /// <summary>
        /// 웹사이트에 (완)제품 목록을 최대 4개까지 보여준다
        /// 작성자 : 지현
        /// </summary>
        public List<ItemVO> GetProductListPage(int page, int page_size)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"select ItemID, ItemName, ItemPrice, ItemCategory, ItemImage, ItemExplain 
from ( 
		select ItemID, ItemName, ItemPrice, ItemCategory, ItemImage, ItemExplain 
				, row_number() over(order by ItemID) as RowNum 
				from TB_Item 
                where ItemCategory = '완제품' 
) A where RowNum between ((@page-1) * @page_size) + 1 and (@page * @page_size)"
            })
            {
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@page_size", page_size);

                cmd.Connection.Open();
                List<ItemVO> list = Helper.DataReaderMapToList<ItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
        /// <summary>
        /// (완)제품의 전체 갯수
        /// 작성자 : 지현
        /// </summary>
        /// <returns></returns>
        public int GetProductTotalCount()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // = strConn;
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "select count(*) from TB_Item  where ItemCategory='완제품'";

                cmd.Connection.Open();
                int n = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Connection.Close();
                return n;
            }
        }
        /// <summary>
        /// 장바구니에 추가한 (완)제품의 ID로, 해당 제품의 정보를 가져와라
        /// 작성자 : 지현
        /// </summary>
        /// <returns></returns>
        public ItemVO GetProductInfo(string id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select ItemID, ItemName, ItemPrice, ItemCategory, ItemSize, ItemImage, ItemExplain 
                                    from TB_Item  where ItemID = @id";
                cmd.Parameters.AddWithValue("@id", id);


                cmd.Connection.Open();
                List<ItemVO> list = Helper.DataReaderMapToList<ItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }
    }
}