using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class BOMVO
    {
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

    }
}
