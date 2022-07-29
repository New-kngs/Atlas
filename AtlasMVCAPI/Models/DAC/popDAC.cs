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
        /// <summary>
        /// 작업지시서 가져오기
        /// </summary>
        /// <returns></returns>
        public List<OperationVO> GetAllOperation()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select OpID, convert(varchar(20), OpDate, 120) OpDate, resourceYN, PutInYN, op.ItemID, ItemName, OrderID, op.ProcessID, ProcessName,    
                                    PlanQty, OpState, BeginDate,EndDate, EmpID
                                    from TB_Operation op join TB_Process p on op.ProcessID = p.ProcessID
                                    join TB_Item i on op.ItemID = i.ItemID";

                cmd.Connection.Open();
                List<OperationVO> list = Helper.DataReaderMapToList<OperationVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        /// <summary>
        /// 작업지시서 검색 리스트 가져오기
        /// </summary>
        /// <returns></returns>
        public List<OperationVO> GetSearchOperation(string dateFrom, string dateTo)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select OpID, convert(varchar(20), OpDate, 120) OpDate, resourceYN, PutInYN, op.ItemID, ItemName, OrderID, op.ProcessID, ProcessName,    
                                    PlanQty, OpState, BeginDate,EndDate, EmpID
                                    from TB_Operation op join TB_Process p on op.ProcessID = p.ProcessID
                                    join TB_Item i on op.ItemID = i.ItemID
                                    where OpDate Between @dateFrom and @dateTo";

                cmd.Parameters.AddWithValue("@dateFrom", dateFrom );
                cmd.Parameters.AddWithValue("@dateTo", dateTo);

                cmd.Connection.Open();
                List<OperationVO> list = Helper.DataReaderMapToList<OperationVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        /// <summary>
        /// 제품 목록 가져오기
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 거채러ID 가져오기
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 거래처 명 가져오기
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 선택된 제품에 필요한 자재 목록 가져오기
        /// </summary>
        /// <returns></returns>
        public List<BOMVO> GetResourceBOM()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @" select OpID, b.ItemID,ItemName, ParentID,ChildID,UnitQty, PlanQty, (UnitQty * PlanQty) Qty, CurrentQty
                                    from TB_BOM b join TB_Item i on b.ChildID = i.ItemID
                                    join TB_Operation o on b.ItemID = o.ItemID";

                cmd.Connection.Open();
                List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
        /// <summary>
        /// 자재 투입여부 업데이트
        /// </summary>
        /// <param name="OpID"></param>
        /// <returns></returns>
        public bool UpdateResourceYN(string OperID)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Operation set ResourceYN = 'Y' where OpID = @OpID"
            })
            {
                cmd.Parameters.AddWithValue("@OpID", OperID);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }


        /// <summary>
        /// 자재 재고 업데이트
        /// </summary>
        /// <param name="OpID"></param>
        /// <returns></returns>
        public bool UpdateResourceQty(List<BOMVO> qty)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"update TB_Item set CurrentQty = @updateQty where ItemID= @itemID";
                cmd.Parameters.Add("@updateQty", System.Data.SqlDbType.Int);
                cmd.Parameters.Add("@itemID", System.Data.SqlDbType.NVarChar, 50);


                int iRowAffect = 0;
                foreach (BOMVO bom in qty)
                {
                    cmd.Parameters["@updateQty"].Value = bom.CurrentQty - bom.Qty;
                    cmd.Parameters["@itemID"].Value = bom.ChildID;

                    iRowAffect += cmd.ExecuteNonQuery();
                }
                conn.Close();
                return (iRowAffect > 0);
            }
            
        }

        /// <summary>
        /// 작업지시서 공정명 콤보리스트
        /// </summary>
        /// <returns></returns>
        public List<ComboItemVO> GetFailCode()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @" select Code, CodeName, Category 
                                    from TB_CommonCode";

                cmd.Connection.Open();
                List<ComboItemVO> list = Helper.DataReaderMapToList<ComboItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        /// <summary>
        /// 작업종료된 제품 창고입고(자재 update)
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public bool PutInItem(ItemVO item)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = "update TB_Item set CurrentQty = @CurrentQty where ItemID = @ItemID"

            })
            {
                cmd.Parameters.AddWithValue("@CurrentQty", item.CurrentQty + item.CompleteQty);
                cmd.Parameters.AddWithValue("@ItemID", item.ItemID);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }



        public bool InsertFailLog(List<FailVO> failList)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"insert into TB_Fail (ItemID, FailQty, FailCode, OpID, CreateUser)
                                    values(@ItemID, @FailQty, @FailCode, @OpID, @CreateUser)";

                cmd.Parameters.AddWithValue("@ItemID", failList[0].ItemID);
                cmd.Parameters.AddWithValue("@OpID", failList[0].OpID);
                cmd.Parameters.AddWithValue("@CreateUser", failList[0].CreateUser);

                cmd.Parameters.Add("@FailQty", System.Data.SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@FailCode", System.Data.SqlDbType.NVarChar, 50);

                int iRowAffect = 0;
                foreach(FailVO fail in failList)
                {
                    cmd.Parameters["@FailQty"].Value = fail.FailQty;
                    cmd.Parameters["@FailCode"].Value = fail.FailCode;
                    iRowAffect += cmd.ExecuteNonQuery();
                }
                conn.Close();
                return (iRowAffect > 0);
            }
        }

        /// <summary>
        /// 창고 입고 여부 업데이트
        /// </summary>
        /// <param name="OpID"></param>
        /// <returns></returns>
        public bool UpdatePutInYN(string OperID)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Operation set PutInYN = 'Y' where OpID = @OpID"
            })
            {
                cmd.Parameters.AddWithValue("@OpID", OperID);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
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
                cmd.CommandText = @"select CONVERT(varchar(10), EquipID) Code, EquipName CodeName, '설비' Category from TB_Equipment";

                cmd.Connection.Open();
                List<ComboItemVO> list = Helper.DataReaderMapToList<ComboItemVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public List<FailVO> GetOperID()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select OpID from TB_Fail";

                cmd.Connection.Open();
                List<FailVO> list = Helper.DataReaderMapToList<FailVO>(cmd.ExecuteReader());
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