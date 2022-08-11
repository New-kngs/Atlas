using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class FailDAC
    {
        string strConn;
        public FailDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        /// <summary>
        /// 불량리스트 가져오기
        /// </summary>
        /// <returns></returns>
        public List<FailVO> GetFailList()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select FailID, OpID, i.ItemID,ItemName, ItemCategory, CodeName FailName, FailCode, FailQty
                                    from TB_Fail f join TB_Item i on f.ItemID = i.ItemID
                                    join TB_CommonCode c on f.FailCode = c.Code";

                cmd.Connection.Open();
                List<FailVO> list = Helper.DataReaderMapToList<FailVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        /// <summary>
        /// 불량리스트 가져오기
        /// </summary>
        /// <returns></returns>
        public List<FailVO> GetFailSearchList(string from, string to)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select FailID, OpID, i.ItemID,ItemName, ItemCategory,CodeName FailName, FailCode, FailQty,
                                    CONVERT(varchar(30), f.CreateDate, 120) CreateDate, f.CreateUser, CONVERT(varchar(30), f.ModifyDate, 120) ModifyDate, f.ModifyUser
                                    from TB_Fail f join TB_Item i on f.ItemID = i.ItemID
                                    join TB_CommonCode c on f.FailCode = c.Code
                                    where f.CreateDate Between @From and @To";

                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                cmd.Connection.Open();
                List<FailVO> list = Helper.DataReaderMapToList<FailVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<ComboItemVO> GetFailCode()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select Code, Category, CodeName from TB_CommonCode";

                cmd.Connection.Open();
                List<ComboItemVO> list = Helper.DataReaderMapToList<ComboItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

    }
}