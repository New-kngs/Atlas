using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtlasDTO;

namespace AtlasMVCAPI.Models
{ 
    public class ProductListViewModel
    {
        public IEnumerable<ItemVO> Products { get; set; }
        public PagingInfo Page { get; set; }
    }
}