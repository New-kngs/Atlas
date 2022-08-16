using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/Purchase")]
    public class PurchaseController : ApiController
    {

        //POST : https://localhost:44391/api/Purchase/SavePurchase
        [HttpPost]
        [Route("SavePurchase")]
        public IHttpActionResult SavePurchase(PurchaseMDVO purList)
        {
            try
            {
                StringBuilder sbPur = new StringBuilder();
                StringBuilder sbQty = new StringBuilder();

                List<PurchaseDetailsVO> purDetail = purList.Detail;
                PurchaseVO pur = purList.Master;

                foreach (PurchaseDetailsVO purResult in purDetail)
                {
                    sbPur.Append(purResult.ItemID);
                    sbQty.Append(purResult.Qty);

                    sbPur.Append(',');
                    sbQty.Append(',');
                }
                PurchaseDAC db = new PurchaseDAC();
                bool flag = db.SavePurchase(pur.CustomerID, pur.CreateUser, sbPur.ToString().Trim().TrimEnd(','), sbQty.ToString().TrimEnd(','));

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

        //POST : https://localhost:44391/api/Purchase/UpdatePurchase
        [HttpPost]
        [Route("UpdatePurchase")]
        public IHttpActionResult UpdatePurchase(PurchaseMDVO purList)
        {
            try
            {
                StringBuilder sbPur = new StringBuilder();
                StringBuilder sbQty = new StringBuilder();

                List<PurchaseDetailsVO> purDetail = purList.Detail;
                PurchaseVO pur = purList.Master;

                foreach (PurchaseDetailsVO purResult in purDetail)
                {
                    sbPur.Append(purResult.ItemID);
                    sbQty.Append(purResult.Qty);

                    sbPur.Append(',');
                    sbQty.Append(',');
                }
                PurchaseDAC db = new PurchaseDAC();
                bool flag = db.UpdatePurchase(pur.PurchaseID, pur.ModifyUser, sbPur.ToString().Trim().TrimEnd(','), sbQty.ToString().TrimEnd(','));

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

        //POST : https://localhost:44391/api/Purchase/UpdatePurStateItemQty
        [HttpPost]
        [Route("UpdatePurStateItemQty")]
        public IHttpActionResult UpdatePurStateItemQty(PurchaseVO pur)
        {
            try
            {               
                PurchaseDAC db = new PurchaseDAC();
                bool flag = db.UpdatePurStateItemQty(pur);

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


        //POST : https://localhost:44391/api/Purchase/DeletePurchase
        [HttpPost]
        [Route("DeletePurchase")]
        public IHttpActionResult DeletePurchase(PurchaseVO purId)
        {
            try
            {
                PurchaseDAC db = new PurchaseDAC();
                bool flag = db.DeletePurchase(purId);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "삭제중 오류발생" : "S"
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
        public IHttpActionResult GetAllPurchase()
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
                PurchaseVO pur = db.GeTPurchaseById(id);

                ResMessage<PurchaseVO> result = new ResMessage<PurchaseVO>()
                {
                    ErrCode = (pur == null) ? -9 : 0,
                    ErrMsg = (pur == null) ? "조회중 오류발생" : "S",
                    Data = pur
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

        // Get : https://localhost:44391/api/Purchase/GetAllPurchaseDetail
        [Route("GetAllPurchaseDetail")]
        public IHttpActionResult GetAllPurchaseDetail()
        {
            try
            {
                PurchaseDAC db = new PurchaseDAC();
                List<PurchaseDetailsVO> list = db.GetAllPurchaseDetail();

                ResMessage<List<PurchaseDetailsVO>> result = new ResMessage<List<PurchaseDetailsVO>>()
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
