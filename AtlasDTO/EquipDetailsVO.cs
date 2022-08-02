using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class EquipDetailsVO
    {
       public int ProcessID { get; set; }
       public int EquipID { get; set; }
        public string CreateUser { get; set; }

        public string EquipName { get; set; }
        public string EquipCategory { get; set; }

    }
}
