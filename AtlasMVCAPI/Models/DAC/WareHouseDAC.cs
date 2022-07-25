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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>모든 창고정보를 가져오기</returns>
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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="whid"></param>
        /// <returns>선택한 창고의 상세정보 가져오기 </returns>
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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="wareHouse"></param>
        /// <returns>선택한 창고를 미사용으로 처리</returns>
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
                cmd.Parameters.AddWithValue("@ModifyUser", wareHouse.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="wareHouse"></param>
        /// <returns>미사용중인 창고를 사용처리</returns>
        public bool UsingWareHouse(WareHouseVO wareHouse)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Warehouse set StateYN = 'Y', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where WHID = @WHID"
            })
            {
                cmd.Parameters.AddWithValue("@WHID", wareHouse.WHID);
                cmd.Parameters.AddWithValue("@ModifyUser", wareHouse.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="wareHouse"></param>
        /// <returns>창고정보 수정</returns>
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
                cmd.Parameters.AddWithValue("@ModifyUser", wareHouse.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="wareHouse"></param>
        /// <returns>새로운 창고 생성</returns>
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
                cmd.Parameters.AddWithValue("@CreateUser", wareHouse.CreateUser);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
    }
}