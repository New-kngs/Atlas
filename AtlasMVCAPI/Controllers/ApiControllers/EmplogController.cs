using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtlasMVCAPI.Models;
using AtlasDTO;

namespace AtlasMVCAPI.Controllers
{
    
    [RoutePrefix("api/Emplog")]
    public class EmplogController : ApiController
    {/// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
        [Route("GetEmplog")]
        public IHttpActionResult GetAllEmployee()
        {
            try
            {
                EmplogDAC db = new EmplogDAC();
                List<EmplogVO> list = db.GetEmplog();

                ResMessage<List<EmplogVO>> result = new ResMessage<List<EmplogVO>>()
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
      /// <summary>
      /// 
      /// </summary>
      /// <param name="emp"></param>
      /// <returns></returns>
        [HttpPost]
        [Route("SaveEmplog")]
        public IHttpActionResult SaveEmplog(EmplogVO emp)
        {
            try
            {
                EmplogDAC db = new EmplogDAC();
                bool flag = db.SaveEmplog(emp);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "저장중 오류발생" : "S"
                };

                return Ok(result);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = err.Message
                });
            }
        }

    }
}
