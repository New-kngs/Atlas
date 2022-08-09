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

        //POST : https://localhost:44391/api/Purchase/SavePurchase
        [HttpPost]
        [Route("SavePurchase")]
        public IHttpActionResult SavePurchase(PurchaseVO pur, List<PurchaseDetailsVO> purDetail)
        {
            try
            {
                PurchaseDAC db = new PurchaseDAC();
                bool flag = db.SavePurchase(pur, purDetail);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "저장중 오류발생" : "S"
                };

                return Ok(result);
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = err.Message
                });
            }
        }

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

        [HttpGet]
        [Route("GetSearchPurchase/{From}/{To}")]        
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


        [HttpGet]
        [Route("GetPurchaseName/{id}")]
        public IHttpActionResult GetPurchaseName(string id)
        {
            try
            {
                PurchaseDAC db = new PurchaseDAC();
                PurchaseVO order = db.GetPurchaseName(id);

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
    }
}
