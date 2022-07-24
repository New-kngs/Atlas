using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasMVCAPI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }  //총 데이터건수
        public int ItemsPerPage { get; set; } //한 페이지당 목록 건수
        public int CurrentPage { get; set; }  //현재 페이지 번호

        //전체 페이지수
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}