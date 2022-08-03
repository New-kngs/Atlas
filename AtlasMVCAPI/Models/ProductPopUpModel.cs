using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasMVCAPI.Models
{
    public class ProductPopUpModel
    {
        public ItemVO Product { get; set; }
        public IEnumerable<BOMVO> BOM { get; set; }
    }
}