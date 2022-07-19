using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class WareHouseVO
    {
        //WHID, WHName, ItemCategory, CreateDate, CreateUser, ModifyDate, ModifyUser, DeletedYN
        public int WHID { get; set; }
        public int WHName { get; set; }
        public int ItemCategory { get; set; }
        public int CreateDate { get; set; }
        public int CreateUser { get; set; }
        public int ModifyDate { get; set; }
        public int ModifyUser { get; set; }
        public int DeletedYN { get; set; }
    }
}
