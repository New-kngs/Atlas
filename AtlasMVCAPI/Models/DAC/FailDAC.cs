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
                                    where CONVERT(varchar(10), f.CreateDate, 120) Between @From and @To
                                     order by OpID";

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
        /// <summary>
        /// 일일 생산 제품별로 다양한 불량 이유를 가져온다 (작성자: 지현)
        /// </summary>
        public List<FailRateChartVO> GetFailRate(string searchDate)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select i.ItemID,ItemName,
       sum(isnull(CompleteQty, 0)) CompleteQty, 
       sum(isnull(OF_Qty, 0)) OF_Qty, sum(isnull(EF_Qty, 0)) EF_Qty, 
      sum(isnull(SF_Qty, 0)) SF_Qty, sum(isnull(IF_Qty, 0)) IF_Qty
from
(
   select f.OpID, f.ItemID
      ,max(CompleteQty) as CompleteQty 
      ,sum(case when f.FailCode = 'OF' then f.FailQty else 0 end) OF_Qty 
      ,sum(case when f.FailCode = 'EF' then f.FailQty else 0 end) EF_Qty 
      ,sum(case when f.FailCode = 'SF' then f.FailQty else 0 end) SF_Qty 
      ,sum(case when f.FailCode = 'IF' then f.FailQty else 0 end) IF_Qty 
      ,max(convert(varchar(10), OpDate, 23)) OpDate 
   from TB_Fail f join TB_Operation o on f.OpID = o.OpID 
   left outer join TB_CommonCode c on f.FailCode = c.Code 
   where convert(varchar(10), OpDate, 120) = @searchDate 
   group by f.OpID, f.ItemID 
) A right outer join TB_Item i on A.ItemID = i.ItemID 
where i.ItemCategory = '완제품' 
group by i.ItemID,ItemName, OpDate";
                cmd.Parameters.AddWithValue("@searchDate", searchDate);

                cmd.Connection.Open();
                // ItemName, CodeName, FailQty
                List<FailRateChartVO> list = Helper.DataReaderMapToList<FailRateChartVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

    }
}