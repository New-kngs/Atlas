using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class LOTVO
    {
        public string LOTID { get; set; }
        public string OrderID { get; set; }
        public string ItemID { get; set; }
        public int LOTIQty { get; set; }
        public int BarCodeID { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }

    }
}
