using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class DepartmentVO
    {
        public int DeptID { get; set; }

        public string DeptName { get; set; }

        public string DeptN { get; set; }

        public string CreateDate { get; set; }

        public string CreateUser { get; set; }

        public string ModifyDate { get; set; }

        public string ModifyUser { get; set; }

        public string DBType { get; set; } // "UPD", "INS", "DEL"


    }
}
