﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class OrderDetailLong
    {
        public string OrderID { get; set; }
        public string ItemID { get; set; }
        public long Qty { get; set; }

        public string ItemName { get; set; }


        // 추가(web)
        public long Num { get; set; }
        public string ItemSize { get; set; }
        public long ItemPrice { get; set; }
        public long SumQty { get; set; }
    }
}
