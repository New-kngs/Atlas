using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDTO
{
    public class OrderVO
    {
        public string OrderID { get; set; } 
        public string CustomerID { get; set; }
        public string OrderShip { get; set; }
        public string OrderEndDate { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }

        // 추가
        public string CustomerName { get; set; }

        // 추가(지현)
        public int price { get; set; }
    }
}
