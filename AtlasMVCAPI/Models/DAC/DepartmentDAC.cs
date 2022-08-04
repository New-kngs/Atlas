using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AtlasDTO;

namespace AtlasMVCAPI.Models
{
    public class DepartmentDAC
    {
        string strConn;
        public DepartmentDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<DepartmentVO> GetDepartmentAll()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select DeptID,DeptName,DeptN,convert(nvarchar(20), CreateDate,120) as CreateDate,CreateUser,convert(nvarchar(20), ModifyDate,120) as ModifyDate,ModifyUser
                                    from TB_Department";

                cmd.Connection.Open();
                List<DepartmentVO> list = Helper.DataReaderMapToList<DepartmentVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }


        public bool UpdateDepart(List<DepartmentVO> Datas)
        {

            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();


            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    int iRowAffect = 0;
                    foreach (DepartmentVO data in Datas)
                    {
                  
                        if (data.DBType == "INS")
                        {
                            cmd.CommandText = @"insert into TB_Department(DeptName, DeptN, CreateDate, CreateUser) values(@DeptName,@DeptN, @CreateDate, @CreateUser)";
                          
                            cmd.Parameters.AddWithValue("@DeptName", data.DeptName);
                            cmd.Parameters.AddWithValue("@DeptN", data.DeptN);
                            cmd.Parameters.AddWithValue("@CreateUser", data.CreateUser);
                            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);

                            iRowAffect += cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();

                        }

                        if(data.DBType == "UPS")
                        {
                            cmd.CommandText = @"Update TB_Department set DeptName = @DeptName, DeptN = @DeptN, ModifyDate= @ModifyDate, ModifyUser = @ModifyUser where DeptID = @DeptID";


                            cmd.Parameters.AddWithValue("@DeptName", data.DeptName);
                            cmd.Parameters.AddWithValue("@DeptN", data.DeptN);
                            cmd.Parameters.AddWithValue("@ModifyUser", data.ModifyUser);
                            cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@DeptID", data.DeptID);

                            iRowAffect += cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();

                        }

                        if(data.DBType == "DEL")
                        {
                            cmd.CommandText = @"delete from TB_Department where DeptID = @DeptID";

                            cmd.Parameters.AddWithValue("@DeptID", data.DeptID);
                            iRowAffect += cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();
                        }
                    }

                    trans.Commit();
                    return (iRowAffect > 0);
                }

            }
            catch (Exception err)
            {
                trans.Rollback();

                string msg = err.Message;

                return false;
            }
            finally
            {
                conn.Close();
            }
        }



        public bool SaveProcessEquip(List<EquipDetailsVO> equip)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();

            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"delete from TB_EquipmentDetails where ProcessID=@ProcessID";
                    cmd.Transaction = trans;
                    cmd.Parameters.AddWithValue("@ProcessID", equip[0].ProcessID);

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"insert into TB_EquipmentDetails(ProcessID, EquipID, EquipName, CreateDate, CreateUser)
                                     values(@ProcessID, @EquipID, @EquipName, @CreateDate, @CreateUser)";

                    cmd.Parameters.Add("@EquipID", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@EquipName", System.Data.SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@CreateUser", System.Data.SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@CreateDate", System.Data.SqlDbType.DateTime);

                    int iRowAffect = 0;
                    foreach (EquipDetailsVO item in equip)
                    {
                        cmd.Parameters["@EquipID"].Value = item.EquipID;
                        cmd.Parameters["@EquipName"].Value = item.EquipName;
                        cmd.Parameters["@CreateDate"].Value = DateTime.Now;
                        cmd.Parameters["@CreateUser"].Value = item.CreateUser;

                        iRowAffect += cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return (iRowAffect > 0);
                }
            }
            catch (Exception err)
            {
                string sss = err.Message;
                trans.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }

        }

    }
}