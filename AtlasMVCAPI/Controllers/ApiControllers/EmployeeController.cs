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

        // Get : https://localhost:44391/api/Employee/DomainCategory
        [Route("DomainCategory")]
        public IHttpActionResult GetDomainCategory()
        {
            try
            {
                EmployeeDAC db = new EmployeeDAC();
                List<ComboItemVO> list = db.GetDomainCategory();

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
                System.Diagnostics.Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        //POST : https://localhost:44391/api/Employee/SaveEmployee
        [HttpPost]
        [Route("SaveEmployee")]
        public IHttpActionResult SaveEmployee(EmployeeVO emp)
        {
            try
            {
                EmployeeDAC db = new EmployeeDAC();
                bool flag = db.SaveEmployee(emp);

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

        //POST : https://localhost:44391/api/Employee/UpdateEmployee
        [HttpPost]
        [Route("UpdateEmployee")]
        public IHttpActionResult UpdateEmployee(EmployeeVO emp)
        {
            try
            {
                EmployeeDAC db = new EmployeeDAC();
                bool flag = db.UpdateEmployee(emp);

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

        //POST : https://localhost:44391/api/Employee/DeleteEmployee
        [HttpPost]
        [Route("DeleteEmployee")]
        public IHttpActionResult DeleteEmployee(EmployeeVO emp)
        {
            try
            {
                EmployeeDAC db = new EmployeeDAC();
                bool flag = db.DeleteEmployee(emp);

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
