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
    {
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

    }
}
