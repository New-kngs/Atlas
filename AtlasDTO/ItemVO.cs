﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class ItemVO
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string CustomerID { get; set; } // 
        public int CurrentQty { get; set; }
        public int SafeQty { get; set; }
        public string WHID { get; set; } //
        public int ItemPrice { get; set; }
        public string ItemCategory { get; set; }
        public string ItemSize { get; set; }
        public string ItemImage { get; set; }
        public string ItemExplain { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
        public string StateYN { get; set; }

        // 추가
        public string CustomerName { get; set; } 
        public string WHName { get; set; }
        public string p_ItemCode { get; set; }

        public int CompleteQty { get; set; }
        
        public string OpID { get; set; }


    }
}
