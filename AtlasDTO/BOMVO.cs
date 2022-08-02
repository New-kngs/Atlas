using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class BOMVO
    {
        //BOMID, B.ItemID, ParentID, ChildID, UnitQty, B.CreateDate, B.CreateUser, B.ModifyDate, B.ModifyUser, B.StateYN, ItemName, ItemCategory, ItemSize
        public string BOMID { get; set; }
        public string ItemID { get; set; }
        public string ParentID { get; set; }
        public string ChildID { get; set; }
        public int UnitQty { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
        public string StateYN { get; set; }

        //추가------------------------------
        public string ItemName { get; set; }
        public int PlanQty { get; set; }
        public int Qty { get; set; }
        public int CurrentQty { get; set; }
        public string ItemCategory { get; set; }
        public string ItemSize { get; set; }
        public string OpID { get; set; }

        public string INFO { get; set; }
        public int LEVELS { get; set; }
        public string sortOrder { get; set; }


        //INFO, B.ItemID, T.ItemName, B.UnitQty, LEVELS, sortOrder

    }
}
