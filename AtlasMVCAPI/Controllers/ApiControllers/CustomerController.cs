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

    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {

        /// <summary>
        /// 등록된 거래처중 입고처만 조회해서 반환
        /// </summary>
        /// <returns></returns>
        //https://localhost:44391/api/Customer/CustomerType
        [Route("CustomerType")]
        public IHttpActionResult GetCustomerType()
        {
            try
            {
                CustomerDAC db = new CustomerDAC();
                List<CustomerVO> list = db.GetCustomerType();

                ResMessage<List<CustomerVO>> result = new ResMessage<List<CustomerVO>>()
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
