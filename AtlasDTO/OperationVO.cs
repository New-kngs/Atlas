﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class OperationVO
    {
        public string OpID { get; set; }
        public string OpDate { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string OrderID { get; set; }
        public int PlanQty { get; set; }
        public int CompleteQty { get; set; }
        public int FailQty { get; set; }
        public string OpState { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
        public string ProcessName { get; set; }
        public string PutInYN { get; set; }
        public string LapingYN { get; set; }
        public string resourceYN { get; set; }
        public int ProcessID { get; set; }
        public string port { get; set; }


        public int PlanID { get; set; }

    }
}
