using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class FailRateChartVO
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public int CompleteQty { get; set; }
        public int OF_Qty { get; set; }
        public int EF_Qty { get; set; }
        public int SF_Qty { get; set; }
        public int IF_Qty { get; set; }
    }
}
