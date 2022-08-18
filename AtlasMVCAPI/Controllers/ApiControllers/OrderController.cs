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
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        // Get : https://localhost:44391/api/Order/GetAllOrder
        [Route("GetAllOrder")]
        public IHttpActionResult GetAllOrder()
        {
            try
            {
                OrderDAC db = new OrderDAC();
                List<OrderVO> list = db.GetAllOrder();

                ResMessage<List<OrderVO>> result = new ResMessage<List<OrderVO>>()
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

        // Get : https://localhost:44391/api/Order/{id}
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GeTOrderById(string id)
        {
            try
            {
                OrderDAC db = new OrderDAC();
                OrderVO order = db.GeTOrderById(id);

                ResMessage<OrderVO> result = new ResMessage<OrderVO>()
                {
                    ErrCode = (order == null) ? -9 : 0,
                    ErrMsg = (order == null) ? "조회중 오류발생" : "S",
                    Data = order
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

        [Route("GetSearchOrder/{From}/{To}")]
        [HttpGet]
        public IHttpActionResult GetSearchOrder(string From, string To)
        {
            try
            {
                OrderDAC db = new OrderDAC();
                List<OrderVO> list = db.GetSearchOrder(From, To);

                ResMessage<List<OrderVO>> result = new ResMessage<List<OrderVO>>()
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


        // Get : https://localhost:44391/api/Order/GetAllOrderDetail
        [Route("GetAllOrderDetail")]
        public IHttpActionResult GetAllOrderDetail()
        {
            try
            {
                OrderDAC db = new OrderDAC();
                List<OrderDetailVO> list = db.GetAllOrderDetail();

                ResMessage<List<OrderDetailVO>> result = new ResMessage<List<OrderDetailVO>>()
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

        //POST : https://localhost:44391/api/Order/OrderEnd
        [HttpPost]
        [Route("OrderEnd")]
        public IHttpActionResult OrderEnd(OrderVO vo)
        {
            try
            {
                OrderDAC db = new OrderDAC();
                bool flag = db.OrderEnd(vo);

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


        // Get : https://localhost:44391/api/Order/GetRptOrder
        [Route("GetRptOrder")]
        public IHttpActionResult GetRptOrder()
        {
            try
            {
                OrderDAC db = new OrderDAC();
                List<RptOrderVO> list = db.GetRptOrder();

                ResMessage<List<RptOrderVO>> result = new ResMessage<List<RptOrderVO>>()
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
