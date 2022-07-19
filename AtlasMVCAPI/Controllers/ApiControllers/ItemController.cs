using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtlasMVCAPI.Models;
using AtlasDTO;
using System.Diagnostics;

namespace AtlasMVCAPI.Controllers.ApiControllers
{
    [RoutePrefix("api/Item")]
    public class ItemController : ApiController
    {
        // Get : https://localhost:44391/api/Item
        [Route("AllItem")]
        public IHttpActionResult GetallItem()
        {
            try
            {
                ItemDAC db = new ItemDAC();
                List<ItemVO> list = db.GetAllItem();

                ResMessage<List<ItemVO>> result = new ResMessage<List<ItemVO>>()
                {
                    ErrCode = (list == null) ? -9 : 0,
                    ErrMsg = (list == null) ? "조회중 오류발생" : "S",
                    Data = list
                };
                return Ok(result);
            }
            catch (Exception err)
            {                
                Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }
    }
}
