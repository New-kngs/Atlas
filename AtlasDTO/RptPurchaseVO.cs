using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class RptPurchaseVO
    {
        public string PurchaseID { get; set; }
        public string CustomerName { get; set; }
        public string PurchaseEndDate { get; set; }
        public string InState { get; set; }
        public string CreateDate { get; set; }
        public string ItemName { get; set; }
        public int? Qty { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EmpName { get; set; }
        public int? ItemPrice { get; set; }

        public string totPrice { get; set; }

    }
}
