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

    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        /// <summary>
        /// 등록된 모든 사용자를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/Employee/AllEmployee
        [Route("AllEmployee")]
        public IHttpActionResult GetAllEmployee()
        {
            try
            {
                EmployeeDAC db = new EmployeeDAC();
                List<EmployeeVO> list = db.GetAllEmployee();

                ResMessage<List<EmployeeVO>> result = new ResMessage<List<EmployeeVO>>()
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
