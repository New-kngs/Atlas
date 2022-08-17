using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class PlanDAC
    {
        string strConn;
        public PlanDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<OrderVO> GetReadyList(string from, string to)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText =
                    @"with orderlist as
                     (
                     select OrderID, CustomerName, convert(varchar(30), O.CreateDate, 120) CreateDate, O.CreateUser
                     from TB_Order O inner join TB_Customer C on O.CustomerID = C.CustomerID
                     )
                     select O.OrderID, CustomerName, convert(varchar(30), O.CreateDate, 120) CreateDate, O.CreateUser
                     from orderlist O inner join (select OrderID from TB_Order except select OrderID from TB_Plan) T 
                          on O.OrderID = T.OrderID
                     where CreateDate Between @From and @To";

                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);

                cmd.Connection.Open();
                List<OrderVO> list = Helper.DataReaderMapToList<OrderVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        
        public List<PlanVO> GetOrderDetail(string order)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText =
                    @"select OD.OrderID, OD.ItemID, OD.Qty, I.ItemName, I.CurrentQty, I.SafeQty, 
                        case when (OD.Qty + I.SafeQty - I.CurrentQty ) > 0 then OD.Qty + I.SafeQty - I.CurrentQty else 0 end NeedQty
                      from TB_OrderDetails OD inner join TB_Item I on OD.ItemID = I.ItemID
                      where OrderID = @OrderID";

                cmd.Parameters.AddWithValue("@OrderID", order);

                cmd.Connection.Open();
                List<PlanVO> list = Helper.DataReaderMapToList<PlanVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        //public List<BOMVO> GetComponents(string item)
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.Connection = new SqlConnection(strConn);
        //        cmd.CommandText =
        //            @"with BOM_CTE as
        //            (
        //            select ItemID, ParentID, UnitQty, 0 levels, cast(ItemID as varchar(30)) sortOrder
        //            from TB_BOM where ItemID = @ItemID
        //            UNION ALL
        //            select C.ItemID, C.ParentID, C.UnitQty, (P.levels +1) levels, cast(P.sortOrder + '>' + C.ItemID as varchar(30)) sortOrder
        //            from TB_BOM C inner join BOM_CTE P on C.ParentID = P.ItemID
        //            )                    
        //            select distinct ItemName, ItemCategory, ItemSize, CurrentQty, SafeQty,
        //                   B.ItemID, T.ItemName, B.UnitQty, LEVELS, sortOrder
        //            from BOM_CTE B join TB_Item T on B.ItemID = T.ItemID
        //            order by sortOrder";

        //        cmd.Parameters.AddWithValue("@ItemID", item);

        //        cmd.Connection.Open();
        //        List<BOMVO> list = Helper.DataReaderMapToList<BOMVO>(cmd.ExecuteReader());
        //        cmd.Connection.Close();

        //        return list;
        //    }
        //}
        public List<PlanVO> GetComponents(string order, string item)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText =
                    @"with BOM_CTE as
                    (
                    select ItemID, ParentID, UnitQty, 0 levels, cast(ItemID as varchar(30)) sortOrder
                    from TB_BOM where ItemID = @ItemID
                    UNION ALL
                    select C.ItemID, C.ParentID, C.UnitQty, (P.levels +1) levels, cast(P.sortOrder + '>' + C.ItemID as varchar(30)) sortOrder
                    from TB_BOM C inner join BOM_CTE P on C.ParentID = P.ItemID
                    )                    
                    select distinct ItemName, ItemCategory, ItemSize, CurrentQty, SafeQty, B.ItemID, T.ItemName, LEVELS, sortOrder,
                    	   ((select case when (OD.Qty + I.SafeQty - I.CurrentQty ) > 0 then OD.Qty + I.SafeQty - I.CurrentQty else 0 end planA 
                            from TB_OrderDetails OD inner join TB_Item I on OD.ItemID = I.ItemID 
                            where OrderID = @OrderID and OD.ItemID = @ItemID) * B.UnitQty) PlanQty,
                    		case when (((select case when (OD.Qty + I.SafeQty - I.CurrentQty ) > 0 then OD.Qty + I.SafeQty - I.CurrentQty else 0 end planA 
                            from TB_OrderDetails OD inner join TB_Item I on OD.ItemID = I.ItemID 
                            where OrderID = @OrderID and OD.ItemID = @ItemID) * B.UnitQty) + SafeQty - CurrentQty ) > 0 then ((select case when (OD.Qty + I.SafeQty - I.CurrentQty ) > 0 then OD.Qty + I.SafeQty - I.CurrentQty else 0 end planA
                    		from TB_OrderDetails OD inner join TB_Item I on OD.ItemID = I.ItemID 
                            where OrderID = @OrderID and OD.ItemID = @ItemID) * B.UnitQty) + SafeQty - CurrentQty else 0 end NeedQty
                    from BOM_CTE B join TB_Item T on B.ItemID = T.ItemID";

                cmd.Parameters.AddWithValue("@OrderID", order); 
                cmd.Parameters.AddWithValue("@ItemID", item);
                

                cmd.Connection.Open();
                List<PlanVO> list = Helper.DataReaderMapToList<PlanVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        
        public bool SavePlanShip(PlanVO list)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = "SP_CreateLOT_BarCodeID",
                CommandType = CommandType.StoredProcedure
            })
            {
                cmd.Parameters.AddWithValue("@ItemID", list.ItemID);
                cmd.Parameters.AddWithValue("@LOTIQty", list.LOTIQty);
                cmd.Parameters.AddWithValue("@CreateUser", list.CreateUser);
                cmd.Parameters.AddWithValue("@OrderID", list.OrderID);
                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public List<PlanVO> GetLOTList(string order)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText =
                    @"select LOTID, OrderID, ItemID, LOTIQty, BarCodeID, CreateDate, CreateUser, ModifyDate, ModifyUser
                     from TB_LOT
                     where OrderID = @OrderID";

                cmd.Parameters.AddWithValue("@OrderID", order);

                cmd.Connection.Open();
                List<PlanVO> list = Helper.DataReaderMapToList<PlanVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }

        public bool SavePlanAdd(PlanVO list)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"insert into TB_Plan (ItemID, PlanQty, CreateDate, CreateUser) 
                                values (@ItemID, @PlanQty, @CreateDate, @CreateUser)",
            })
            {
                cmd.Parameters.AddWithValue("@ItemID", list.ItemID);
                cmd.Parameters.AddWithValue("@PlanQty", list.PlanQty);
                cmd.Parameters.AddWithValue("@CreateUser", list.CreateUser);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool SavePlanPlan(PlanOptVO list)
        {            
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                //CommandText = @"insert into TB_Plan (OrderID, ItemID, PlanQty, CreateDate, CreateUser) 
                //                values 
                //                (@OrderID, @ItemID, @PlanQty, @CreateDate, @CreateUser),
                //                (@OrderID, @ItemID1, @PlanQty1, @CreateDate, @CreateUser),
                //                (@OrderID, @ItemID2, @PlanQty2, @CreateDate, @CreateUser);",
                
                
                CommandText = @"insert into TB_Plan (OrderID, ItemID, PlanQty, CreateDate, CreateUser) 
                                values (@OrderID, @ItemID, @PlanQty, @CreateDate, @CreateUser);   

                                insert into TB_Plan (OrderID, ItemID, PlanQty, CreateDate, CreateUser) 
                                values (@OrderID, @ItemID1, @PlanQty1, @CreateDate, @CreateUser);

                                insert into TB_Plan (OrderID, ItemID, PlanQty, CreateDate, CreateUser) 
                                values (@OrderID, @ItemID2, @PlanQty2, @CreateDate, @CreateUser);

                                delete from TB_Plan where PlanQty = 0;",
            })
            {
                
                cmd.Parameters.AddWithValue("@OrderID", list.OrderID);
                cmd.Parameters.AddWithValue("@CreateUser", list.CreateUser);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.Parameters.AddWithValue("@ItemID", list.ProductID);
                cmd.Parameters.AddWithValue("@PlanQty", list.ProductQty);
                
                cmd.Parameters.AddWithValue("@ItemID1", list.Semi1ID);
                cmd.Parameters.AddWithValue("@PlanQty1", list.Semi1Qty);
                
                cmd.Parameters.AddWithValue("@ItemID2", list.Semi2ID);
                cmd.Parameters.AddWithValue("@PlanQty2", list.Semi2Qty);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public List<PlanVO> GetPlanList()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(strConn);
                cmd.CommandText = @"select PlanID, p.ItemID,ItemName, ItemCategory, PlanQty, OrderID, CreateYN, convert(nvarchar(20), p.CreateDate,120) CreateDate,  p.CreateUser,convert(nvarchar(20), p.ModifyDate,120) ModifyDate, p.ModifyUser 
                    from TB_Plan p join TB_Item i on p.ItemID = i.ItemID";

                cmd.Connection.Open();
                List<PlanVO> list = Helper.DataReaderMapToList<PlanVO>(cmd.ExecuteReader());
                cmd.Connection.Close();

                return list;
            }
        }


        public bool SaveOperation(OperationVO oper)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"SP_CreateOperation",
                CommandType = CommandType.StoredProcedure
            })
            {
                cmd.Parameters.AddWithValue("@PlanID", oper.PlanID);
                cmd.Parameters.AddWithValue("@OrderID", oper.OrderID);
                cmd.Parameters.AddWithValue("@ItemID", oper.ItemID);
                cmd.Parameters.AddWithValue("@PlanQty", oper.PlanQty);
                cmd.Parameters.AddWithValue("@CreateUser", oper.CreateUser);
                cmd.Parameters.AddWithValue("@EmpID", oper.CreateUser);
                cmd.Parameters.AddWithValue("@ProcessID", oper.ProcessID);


                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }

        public bool DeletePlan(int planID)
        {
            using (SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(strConn),
                CommandText = @"delete from TB_Plan where PlanID = @PlanID",
            })
            {
                cmd.Parameters.AddWithValue("@PlanID", planID);

                cmd.Connection.Open();
                int iRowAffect = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return (iRowAffect > 0);
            }
        }
    }
}
