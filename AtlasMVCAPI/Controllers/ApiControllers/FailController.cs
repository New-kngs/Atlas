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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // Get : https://localhost:44391/api/Fail/GetFailSearchList
        [Route("GetFailSearchList/{from}/{to}")]
        public IHttpActionResult GetFailSearchList(string from, string to)
        {
            try
            {
                FailDAC db = new FailDAC();
                List<FailVO> list = db.GetFailSearchList(from, to);

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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // Get : https://localhost:44391/api/Fail/GetFailCode
        [Route("GetFailCode")]
        public IHttpActionResult GetFailCode()
        {
            try
            {
                FailDAC db = new FailDAC();
                List<ComboItemVO> list = db.GetFailCode();

                ResMessage<List<ComboItemVO>> result = new ResMessage<List<ComboItemVO>>()
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
