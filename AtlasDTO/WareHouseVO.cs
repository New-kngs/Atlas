using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class WareHouseVO
    {
        //WHID, WHName, ItemCategory, CreateDate, CreateUser, ModifyDate, ModifyUser, StateYN
        public string WHID { get; set; }
        public string WHName { get; set; }
        public string ItemCategory { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
        public string StateYN { get; set; }
    }
}
