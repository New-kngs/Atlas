using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class PurchaseVO
    {
        public string PurchaseID { get; set; }
        public string CustomerID { get; set; }
        public string PurchaseEndDate { get; set; }
        public string InState { get; set; }
        public string WHID { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }

        //
        public string CustomerName { get; set; }
        public string WHName { get; set; }
        public string ItemID { get; set; }
        public int ItemPrice { get; set; }
    }
}
