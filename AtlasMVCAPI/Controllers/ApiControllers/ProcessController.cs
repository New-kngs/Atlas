using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AtlasMVCAPI.Controllers.ApiControllers
{
    [RoutePrefix("api/Process")]
    public class ProcessController : ApiController
    {
        /// <summary>
        /// 등록된 모든 사용자를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/Process/AllProcess
        [Route("AllProcess")]
        public IHttpActionResult GetAllProcess()
        {
            try
            {
                ProcessDAC db = new ProcessDAC();
                List<ProcessVO> list = db.GetAllProcess();

                ResMessage<List<ProcessVO>> result = new ResMessage<List<ProcessVO>>()
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
