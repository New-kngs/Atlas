using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class popDAC
    {
        string strConn;
        public popDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<OperationVO> GetAllOpration()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select OpID, OpDate, ItemID, OrderID, PlanQty, OpState, BeginDate,EndDate, EmpID
                                    from TB_Operation";

                cmd.Connection.Open();
                List<OperationVO> list = Helper.DataReaderMapToList<OperationVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<ItemVO> GetItem()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select ItemID,ItemName, CustomerID, ItemCategory, WHID
                                  from TB_Item";

                cmd.Connection.Open();
                List<ItemVO> list = Helper.DataReaderMapToList<ItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }


        public List<OrderVO> GetCustomerID()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select OrderID, CustomerID from TB_Order";

                cmd.Connection.Open();
                List<OrderVO> list = Helper.DataReaderMapToList<OrderVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<CustomerVO> GetCustomerName()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select CustomerID, CustomerName from TB_Customer";

                cmd.Connection.Open();
                List<CustomerVO> list = Helper.DataReaderMapToList<CustomerVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<BOMVO> GetResourceBOM()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select b.ItemID,ItemName, ParentID,ChildID,UnitQty, PlanQty, (UnitQty * PlanQty) Qty
                                    from TB_BOM b join TB_Item i on b.ChildID = i.ItemID
                                    join TB_Operation o on b.ItemID = o.ItemID";

                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public bool SaveProcess(ProcessVO process)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"insert into TB_Process (ProcessName, FailCheck, CreateUser)
                                values ( @ProcessName,@FailCheck, @CreateUser)"

            })
            {
                cmd.Parameters.AddWithValue("@ProcessName", process.ProcessName);
                cmd.Parameters.AddWithValue("@FailCheck", process.FailCheck);
                cmd.Parameters.AddWithValue("@CreateUser", process.CreateUser);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UpdateProcess(ProcessVO process)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Process set ProcessName = @ProcessName, FailCheck = @FailCheck, ModifyDate=@ModifyDate, ModifyUser = @ModifyUser
                                where ProcessID = @ProcessID"

            })
            {
                cmd.Parameters.AddWithValue("@ProcessID", process.ProcessID);
                cmd.Parameters.AddWithValue("@ProcessName", process.ProcessName);
                cmd.Parameters.AddWithValue("@FailCheck", process.FailCheck);
                cmd.Parameters.AddWithValue("@ModifyUser", process.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool DeleteProcess(ProcessVO process)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),

                CommandText = @"update TB_Process set StateYN = 'N', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where ProcessID = @ProcessID"

            })
            {
                cmd.Parameters.AddWithValue("@ProcessID", process.ProcessID);
                cmd.Parameters.AddWithValue("@ModifyUser", process.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool UsingProcess(ProcessVO process)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Process set StateYN = 'Y', ModifyDate=@ModifyDate, ModifyUser = @ModifyUser where ProcessID = @ProcessID"

            })
            {
                cmd.Parameters.AddWithValue("@ProcessID", process.ProcessID);
                cmd.Parameters.AddWithValue("@ModifyUser", process.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public List<ComboItemVO> GetEquipName()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @" select CONVERT(varchar(10), EquipID) Code, EquipName CodeName, '설비' Category from TB_Equipment";

                cmd.Connection.Open();
                List<ComboItemVO> list = Helper.DataReaderMapToList<ComboItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
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
            catch(Exception err)
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

        public List<EquipDetailsVO> GetProcessEquip()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select ProcessID, EquipID, EquipName, CreateUser
                                    from TB_EquipmentDetails";

                cmd.Connection.Open();
                List<EquipDetailsVO> list = Helper.DataReaderMapToList<EquipDetailsVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
    }
}