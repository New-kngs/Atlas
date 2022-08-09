using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/Fail")]
    public class FailController : ApiController
    {
        // Get : https://localhost:44391/api/Fail/GetFailList
        [Route("GetFailList")]
        public IHttpActionResult GetFailList()
        {
            try
            {
                FailDAC db = new FailDAC();
                List<FailVO> list = db.GetFailList();

                ResMessage<List<FailVO>> result = new ResMessage<List<FailVO>>()
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
