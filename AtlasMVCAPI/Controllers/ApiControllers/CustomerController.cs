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
        // https://localhost:44391/api/Customer/AllCustomer
        [Route("AllCustomer")]
        public IHttpActionResult GetAllCustomer()
        {
            try
            {
                CustomerDAC db = new CustomerDAC();
                List<CustomerVO> list = db.GetAllCustomer();

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

        // https://localhost:44391/api/Customer/GetCustomerlist
        [Route("GetCustomerlist")]
        public IHttpActionResult GetCustomerlist()
        {
            try
            {
                CustomerDAC db = new CustomerDAC();
                List<CustomerVO> list = db.GetCustomerlist();

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

        //POST : https://localhost:44391/api/Customer/SaveCustomer
        [HttpPost]
        [Route("SaveCustomer")]
        public IHttpActionResult SaveCustomer(CustomerVO vo)
        {
            try
            {
                CustomerDAC db = new CustomerDAC();
                bool flag = db.SaveCustomer(vo);

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

        //POST : https://localhost:44391/api/Customer/UpdateCustomer
        [HttpPost]
        [Route("UpdateCustomer")]
        public IHttpActionResult UpdateCustomer(CustomerVO vo)
        {
            try
            {
                CustomerDAC db = new CustomerDAC();
                bool flag = db.UpdateCustomer(vo);

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

        //POST : https://localhost:44391/api/Customer/DeleteCustomer
        [HttpPost]
        [Route("DeleteCustomer")]
        public IHttpActionResult DeleteCustomer(CustomerVO vo)
        {
            try
            {
                CustomerDAC db = new CustomerDAC();
                bool flag = db.DeleteCustomer(vo);

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
