using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class LoginVO
    {
        // 거래처(Customer)
        public string CustomerID { get; set; }
        public string CustomerPwd { get; set; }
        public string CustomerName { get; set; }
        public string EmpPhone { get; set; }
        public string EmpEmail { get; set; }
        public string DeptName { get; set; }

        // 임원(EIS)
        public string EmpID { get; set; }
        public string EmpName { get; set; }

        // 거래처? 임원? 비회원
        public string State { get; set; }
    }
}
