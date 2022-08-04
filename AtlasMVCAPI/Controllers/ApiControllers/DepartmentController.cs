using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtlasDTO;
using AtlasMVCAPI.Models;

namespace AtlasMVCAPI.Controllers
{

    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
      //https://localhost:44391/api/Depamen/all
        [Route("all")]
        public IHttpActionResult GetDepartmentAll()
        {
            try
            {
                DepartmentDAC db = new DepartmentDAC();
                List<DepartmentVO> list = db.GetDepartmentAll();

                ResMessage<List<DepartmentVO>> result = new ResMessage<List<DepartmentVO>>()
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

        //POST : https://localhost:44391/api/Depamenrtt/UpdateDepart
        [HttpPost]
        [Route("UpdateDepart")]
        public IHttpActionResult UpdateDepart(List<DepartmentVO> data)
        {
            try
            {
                DepartmentDAC db = new DepartmentDAC();
                bool flag = db.UpdateDepart(data);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "삭제 중 오류발생" : "S"
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
