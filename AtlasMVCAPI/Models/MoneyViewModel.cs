using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtlasDTO;

namespace AtlasMVCAPI.Models
{ 
    public class MoneyViewModel
    {
        public string M_Date { get; set; } 
        public List<string> ItemSalesName { get; set; } 
        public List<string> ItemSalePrice { get; set; } 
        public List<string> ItemPurchaseName { get; set; } 
        public List<string> ItemPurchasePrice { get; set; }
    }
}