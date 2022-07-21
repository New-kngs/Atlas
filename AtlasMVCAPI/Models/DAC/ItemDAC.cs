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

        // 일단 보류
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

        //public bool SaveItem(ItemVO item)
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = new SqlConnection(strConn);
        //        cmd.CommandText = @"";


        //        cmd.Parameters.AddWithValue("@", );


        //        cmd.Connection.Open();
        //        int iRowAffect = cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();

        //        return (iRowAffect > 0);
        //    }
        //}




        //public bool SaveItem(ItemVO process)
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = new SqlConnection(strConn);
        //        cmd.CommandText = @"insert into TB_Process (ProcessName, FailCheck, CreateUser)
        //                        values ( @ProcessName,@FailCheck, @CreateUser)";


        //        cmd.Parameters.AddWithValue("@ProcessName", process.ProcessName);
        //        cmd.Parameters.AddWithValue("@FailCheck", process.FailCheck);
        //        cmd.Parameters.AddWithValue("@CreateUser", "김길동");

        //        cmd.Connection.Open();
        //        int iRowAffect = cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();

        //        return (iRowAffect > 0);
        //    }
        //}

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
        public int GetProductInfo()
        {
            return 0;
        }
    }
}