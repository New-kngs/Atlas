using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace AtlasMVCAPI.Controllers
{

    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
      //https://localhost:44391/api/Depament/all
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
