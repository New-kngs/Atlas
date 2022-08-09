using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class FailVO
    {
        public int FailID { get; set; }
        public string ItemID { get; set; }
        public int FailQty { get; set; }
        public string FailCode { get; set; }
        public string FailName { get; set; }
        public string OpID { get; set; }
        public string  ItemName { get; set; }
        public string ItemCategory { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }

    }
}
