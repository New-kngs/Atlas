using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtlasDTO;

namespace AtlasMVCAPI.Models
{ 
    public class FailRateModel
    {
        public string ItemNames { get; set; }
        public string Complete { get; set; } // 정상
        public string OF { get; set; } // 작업실수
        public string EF { get; set; } // 설비고장
        public string SF { get; set; } // 시스템오류
        public string IF { get; set; } // 자재불량
    }
}