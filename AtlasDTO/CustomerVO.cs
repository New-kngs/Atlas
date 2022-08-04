using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class CustomerVO
    {
        public string CustomerID { get; set; }
        public string CustomerPwd { get; set; }
        public string CustomerName { get; set; }
        public string Category { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EmpID { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }

        public string ModifyUser { get; set; }

        public string StateYN { get; set; }

    }
}
