using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class PlanVO
    {
        //PlanID, ItemID, PlanQty, OrderID, ItemCategory, SafeQty, CurrentQty, ItemName, CreateDate, CreateUser, ModifyDate, ModifyUser
        public int PlanID { get; set; }
        public string ItemID { get; set; }
        public int PlanQty { get; set; }
        public string OrderID { get; set; }
        public string ItemCategory { get; set; }
        public int SafeQty { get; set; }
        public int CurrentQty { get; set; }
        public string ItemName { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
    }
}
