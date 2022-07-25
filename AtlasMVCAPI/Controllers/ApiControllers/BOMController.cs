using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/BOM")]

    public class BOMController : ApiController
    {
        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>등록된 BOM의 Item리스트를 조회해서 반환</returns>
        //https://localhost:44391/api/BOM/AllBOMItem
        [Route("AllBOMItem")]
        public IHttpActionResult GetBOMItemList()
        {
            try
            {
                BOMDAC db = new BOMDAC();
                List<BOMVO> list = db.GetBOMItemList();

                ResMessage<List<BOMVO>> result = new ResMessage<List<BOMVO>>()
                {
                    ErrCode = (list == null) ? -9 : 0,
                    ErrMsg = (list == null) ? "조회중 오류발생" : "S",
                    Data = list
                };

                return Ok(result);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }
    }
}
