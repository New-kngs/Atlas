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
        public IEnumerable<string> ItemSalesName { get; set; } 
        public IEnumerable<string> ItemSalePrice { get; set; } 
        public IEnumerable<string> ItemPurchaseName { get; set; } 
        public IEnumerable<string> ItemPurchasePrice { get; set; } 
    }
}