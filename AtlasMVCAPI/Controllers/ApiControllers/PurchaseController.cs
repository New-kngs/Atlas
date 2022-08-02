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
    [RoutePrefix("api/Purchase")]
    public class PurchaseController : ApiController
    {
        // Get : https://localhost:44391/api/Purchase/GetAllPurchase
        [Route("GetAllPurchase")]
        public IHttpActionResult GetAllOrder()
        {
            try
            {
                PurchaseDAC db = new PurchaseDAC();
                List<PurchaseVO> list = db.GetAllPurchase();

                ResMessage<List<PurchaseVO>> result = new ResMessage<List<PurchaseVO>>()
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

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GeTPurchaseById(string id)
        {
            try
            {
                PurchaseDAC db = new PurchaseDAC();
                PurchaseVO order = db.GeTPurchaseById(id);

                ResMessage<PurchaseVO> result = new ResMessage<PurchaseVO>()
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


        [Route("GetSearchPurchase/{From}/{To}")]
        [HttpGet]
        public IHttpActionResult GetSearchPurchase(string From, string To)
        {
            try
            {
                PurchaseDAC db = new PurchaseDAC();
                List<PurchaseVO> list = db.GetSearchPurchase(From, To);

                ResMessage<List<PurchaseVO>> result = new ResMessage<List<PurchaseVO>>()
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
