using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class ProcessVO
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public string FailCheck { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
        public string StateYN { get; set; }

    }
}
