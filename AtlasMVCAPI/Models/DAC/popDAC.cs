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
                cmd.CommandText = @"select OpID, convert(varchar(10), OpDate, 120) OpDate, resourceYN, PutInYN, op.ItemID, ItemName, OrderID, op.ProcessID, ProcessName,    
                    PlanQty, OpState, convert(varchar(20), BeginDate,120) BeginDate,convert(varchar(20), EndDate,120) EndDate, op.EmpID, EmpName, port, CompleteQty, FailQty
                    from TB_Operation op join TB_Process p on op.ProcessID = p.ProcessID
                    join TB_Item i on op.ItemID = i.ItemID
                    join TB_Employees e on e.EmpName = op.EmpID";

                cmd.Connection.Open();
                List<OperationVO> list = Helper.DataReaderMapToList<OperationVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }
        /// <summary>
        /// 설비정보 가져오기
        /// </summary>
        /// <returns></returns>
        public List<EquipDetailsVO> GetEquip()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"  select ProcessID, d.EquipID, d.EquipName, EquipCategory
                                    from TB_EquipmentDetails d join TB_Equipment e on d.EquipID = e.EquipID";

                cmd.Connection.Open();
                List<EquipDetailsVO> list = Helper.DataReaderMapToList<EquipDetailsVO>(cmd.ExecuteReader());
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
                cmd.CommandText = @"select OpID, convert(varchar(10), OpDate, 120) OpDate, resourceYN, PutInYN, op.ItemID, ItemName, OrderID, op.ProcessID, ProcessName,    
                    PlanQty, OpState, convert(varchar(20), BeginDate,120) BeginDate,convert(varchar(20), EndDate,120) EndDate, op.EmpID, EmpName, port, CompleteQty, FailQty
                    from TB_Operation op join TB_Process p on op.ProcessID = p.ProcessID
                    join TB_Item i on op.ItemID = i.ItemID
                    join TB_Employees e on e.EmpName = op.EmpID
                                    where convert(varchar(10), OpDate, 120) Between @dateFrom and @dateTo";

                cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
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
                cmd.CommandText = @" select OpID, b.ItemID,ItemName, ParentID, ChildID,UnitQty, PlanQty, (UnitQty * PlanQty) Qty, CurrentQty
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
                CommandText = @"update TB_Item set CurrentQty = @CurrentQty, ModifyDate = @ModifyDate, ModifyUser = @ModifyUser where ItemID = @ItemID"
            })
            {
                cmd.Parameters.AddWithValue("@CurrentQty", item.CurrentQty);
                cmd.Parameters.AddWithValue("@ItemID", item.ItemID);
                cmd.Parameters.AddWithValue("@ModifyUser", item.ModifyUser);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
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
                foreach (FailVO fail in failList)
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
        public bool UpdatePutInYN(OperationVO oper)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Operation set PutInYN = 'Y' where OpID = @OpID"
            })
            {
                cmd.Parameters.AddWithValue("@OpID", oper.OpID);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

 

        /// <summary>
        /// 작업시작
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool UpdateFinishWorkYN(OperationVO oper)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Operation set OpState = '작업종료', PutInYN = 'Y', BeginDate = @BeginDate, ModifyUser = @ModifyUser, ModifyDate = @ModifyDate
                    where OpID = @OpID"

            })
            {
                cmd.Parameters.AddWithValue("@ModifyUser", oper.EmpID);
                cmd.Parameters.AddWithValue("@OpID", oper.OpID);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@BeginDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
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



        /// <summary>
        /// 작업시작
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool UdateState(OperationVO oper)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Operation set OpState = '작업중', BeginDate = @BeginDate, ModifyUser = @ModifyUser, ModifyDate = @ModifyDate
                    where OpID = @OpID"

            })
            {
                cmd.Parameters.AddWithValue("@ModifyUser", oper.EmpID);
                cmd.Parameters.AddWithValue("@OpID", oper.OpID);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@BeginDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
        /// <summary>
        /// 작업종료
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool UdateFinish(OperationVO oper)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"update TB_Operation set OpState = '입고대기', CompleteQty = @CompleteQty, FailQty = @FailQty, ModifyUser = @ModifyUser, ModifyDate = @ModifyDate, EndDate = @EndDate
                                where OpID = @OpID"
            })
            {
                cmd.Parameters.AddWithValue("@CompleteQty", oper.CompleteQty);
                cmd.Parameters.AddWithValue("@FailQty", oper.FailQty);
                cmd.Parameters.AddWithValue("@ModifyUser", oper.EmpID);
                cmd.Parameters.AddWithValue("@OpID", oper.OpID);
                cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@EndDate", DateTime.Now);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        /// <summary>
        /// LOT, 바코드ID생성
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool CreateLOT(LOTVO lot)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"SP_CreateLOT_BarCodeID",
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                cmd.Parameters.AddWithValue("@ItemID", lot.ItemID);
                cmd.Parameters.AddWithValue("@LOTIQty", lot.LOTIQty);
                cmd.Parameters.AddWithValue("@CreateUser", lot.CreateUser);
                cmd.Parameters.AddWithValue("@OrderID", lot.OrderID);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
        /// <summary>
        /// 포장 리스트 가져오기
        /// </summary>
        /// <returns></returns>
        public List<OperationVO> GetLapingList()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @" select op.OrderID, op.ItemID, ItemName, PlanQty, PutInYN
                                        from TB_Operation op join TB_Item i on op.ItemID = i.ItemID
                                        join TB_Order d on op.OrderID = d.OrderID
                                        where PutInYN = 'Y' and LapingYN = 'N' and OpState = '작업종료'";

                cmd.Connection.Open();
                List<OperationVO> list = Helper.DataReaderMapToList<OperationVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }


    }
}