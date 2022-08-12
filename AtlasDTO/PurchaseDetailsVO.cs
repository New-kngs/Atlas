using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class PurchaseDetailsVO
    {
        public string PurchaseID { get; set; }
        public string ItemID { get; set; }
        public int Qty { get; set; }


        public string ItemName { get; set; }
        public int PurTotPrice { get; set; }
        public string ItemSize { get; set; }
        public string CustomerName { get; set; }
        public int ItemPrice { get; set; }

    }
}
