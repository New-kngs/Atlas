using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models.DAC
{
    public class LoginDAC
    {
        string strConn;
        public LoginDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        /// <summary>
        /// 거래처 로그인 체크 (작성자: 지현)
        /// </summary>
        public LoginVO LoginCheck(string LoginID, string LoginPWD)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = "SP_LoginInfo";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoginID", LoginID);
                cmd.Parameters.AddWithValue("@LoginPWD", LoginPWD);
                cmd.Connection.Open();
                List<LoginVO> list = Helper.DataReaderMapToList<LoginVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }
    }
}