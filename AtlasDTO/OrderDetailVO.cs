﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class OrderDetailVO
    {
        public string OrderID { get; set; }
        public string ItemID { get; set; }
        public int Qty { get; set; }

        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public string ItemSize { get; set; }
    }
}
