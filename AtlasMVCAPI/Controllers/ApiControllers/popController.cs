using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/pop")]
    public class popController : ApiController
    {

        
        /// <summary>
        /// 등록된 모든 공정를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/pop/AllOperation
        [Route("AllOperation")]
        public IHttpActionResult GetAllOpration()
        {
            try
            {
                popDAC db = new popDAC();
                List<OperationVO> list = db.GetAllOperation();

                ResMessage<List<OperationVO>> result = new ResMessage<List<OperationVO>>()
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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                }) ;
            }
        }

        /// <summary>
        /// 등록된 모든 공정를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/pop/GetEquip
        [Route("GetEquip")]
        public IHttpActionResult GetEquip()
        {
            try
            {
                popDAC db = new popDAC();
                List<EquipDetailsVO> list = db.GetEquip();

                ResMessage<List<EquipDetailsVO>> result = new ResMessage<List<EquipDetailsVO>>()
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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }
        /// <summary>
        /// 등록된 모든 공정를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/pop/SearchOper/{dateFrom}/{dateTo}

        [Route("SearchOper/{dateFrom}/{dateTo}")]
        [HttpGet]
        public IHttpActionResult SearchOper(string dateFrom, string dateTo)
        {
            try
            {
                popDAC db = new popDAC();
                List<OperationVO> list = db.GetSearchOperation(dateFrom, dateTo);

                ResMessage<List<OperationVO>> result = new ResMessage<List<OperationVO>>()
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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        /// <summary>
        /// 등록된 모든 제품을 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/pop/getItem
        [Route("getItem")]
        public IHttpActionResult GetItem()
        {
            try
            {
                popDAC db = new popDAC();
                List<ItemVO> list = db.GetItem();

                ResMessage<List<ItemVO>> result = new ResMessage<List<ItemVO>>()
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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        /// <summary>
        /// 등록된 모든 거래처ID를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/pop/GetCustomer
        [Route("GetCustomer")]
        public IHttpActionResult GetCustomerID()
        {
            try
            {
                popDAC db = new popDAC();
                List<OrderVO> list = db.GetCustomerID();

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
                System.Diagnostics.Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        /// <summary>
        /// 등록된 모든 거래처명을 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/pop/GetCustomerName
        [Route("GetCustomerName")]
        public IHttpActionResult GetCustomerName()
        {
            try
            {
                popDAC db = new popDAC();
                List<CustomerVO> list = db.GetCustomerName();

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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        /// <summary>
        /// 제품생산에 필요한 자재수량
        /// </summary>
        //https://localhost:44391/api/pop/GetResourceBOM
        [Route("GetResourceBOM")]
        public IHttpActionResult GetResourceBOM()
        {
            try
            {
                popDAC db = new popDAC();
                List<BOMVO> list = db.GetResourceBOM();

                ResMessage<List<BOMVO>> result = new ResMessage<List<BOMVO>>()
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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        //POST : https://localhost:44391/api/pop/UpdateResourceYN
        [HttpPost]
        [Route("UpdateResourceYN/{OperID}")]
        public IHttpActionResult UpdateResourceYN(string OperID)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.UpdateResourceYN(OperID);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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
        //POST : https://localhost:44391/api/pop/UpdateResourceQty
        [HttpPost]
        [Route("UpdateResourceQty")]
        public IHttpActionResult UpdateResourceQty(List<BOMVO> bom)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.UpdateResourceQty(bom);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

        /// <summary>
        /// 작업지시서 공정명 콤보리스트
        /// </summary>
        //https://localhost:44391/api/pop/GetFailCode
        [Route("GetFailCode")]
        public IHttpActionResult GetFailCode()
        {
            try
            {
                popDAC db = new popDAC();
                List<ComboItemVO> list = db.GetFailCode();

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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        //POST : https://localhost:44391/api/pop/PutInItem
        [HttpPost]
        [Route("PutInItem")]
        public IHttpActionResult PutInItem(ItemVO item)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.PutInItem(item);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

        //POST : https://localhost:44391/api/pop/InsertFailLog
        [HttpPost]
        [Route("InsertFailLog")]
        public IHttpActionResult InsertFailLog(List<FailVO> failList)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.InsertFailLog(failList);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

        //POST : https://localhost:44391/api/pop/UpdatePutInYN
        [HttpPost]
        [Route("UpdatePutInYN")]
        public IHttpActionResult UpdatePutInYN(OperationVO oper)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.UpdatePutInYN(oper);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

        /// <summary>
        /// 등록된 모든 거래처명을 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/pop/GetOperID
        [Route("GetOperID")]
        public IHttpActionResult GetOperID()
        {
            try
            {
                popDAC db = new popDAC();
                List<FailVO> list = db.GetOperID();

                ResMessage<List<FailVO>> result = new ResMessage<List<FailVO>>()
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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        //POST : https://localhost:44391/api/pop/UdateState
        [HttpPost]
        [Route("UdateState")]
        public IHttpActionResult UdateState(OperationVO oper)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.UdateState(oper);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

        //POST : https://localhost:44391/api/pop/UpdateFinishWorkYN
        [HttpPost]
        [Route("UpdateFinishWorkYN")]
        public IHttpActionResult UpdateFinishWorkYN(OperationVO oper)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.UpdateFinishWorkYN(oper);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

        //POST : https://localhost:44391/api/pop/UdateFinish
        [HttpPost]
        [Route("UdateFinish")]
        public IHttpActionResult UdateFinish(OperationVO oper)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.UdateFinish(oper);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

        //POST : https://localhost:44391/api/pop/CreateLOT
        [HttpPost]
        [Route("CreateLOT")]
        public IHttpActionResult CreateLOT(LOTVO lot)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.CreateLOT(lot);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

        /// <summary>
        /// 제품생산에 필요한 자재수량
        /// </summary>
        //https://localhost:44391/api/pop/GetLapingList
        [Route("GetLapingList")]
        public IHttpActionResult GetLapingList()
        {
            try
            {
                popDAC db = new popDAC();
                List<OperationVO> list = db.GetLapingList();

                ResMessage<List<OperationVO>> result = new ResMessage<List<OperationVO>>()
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
                    //ErrMsg = err.Message
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }



    }
    }

