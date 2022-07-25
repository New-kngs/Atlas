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
                List<OperationVO> list = db.GetAllOpration();

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

        //POST : https://localhost:44391/api/Process/UpdateResourceYN
        [HttpPost]
        [Route("UpdateResourceYN")]
        public IHttpActionResult UpdateResourceYN(string ItemID)
        {
            try
            {
                popDAC db = new popDAC();
                bool flag = db.UpdateResourceYN(ItemID);

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



        //POST : https://localhost:44391/api/Process/SaveProcess
        [HttpPost]
        [Route("SaveProcess")]
        public IHttpActionResult SaveProcess(ProcessVO process)
        {
            try
            {
                ProcessDAC db = new ProcessDAC();
                bool flag = db.SaveProcess(process);

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


        //POST : https://localhost:44391/api/Process/UpdateProcess
        [HttpPost]
        [Route("UpdateProcess")]
        public IHttpActionResult UpdateProcess(ProcessVO process)
        {
            try
            {
                ProcessDAC db = new ProcessDAC();
                bool flag = db.UpdateProcess(process);

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

        //POST : https://localhost:44391/api/Process/DeleteProcess
        [HttpPost]
        [Route("DeleteProcess")]
        public IHttpActionResult DeleteProcess(ProcessVO process)
        {
            try
            {
                ProcessDAC db = new ProcessDAC();
                bool flag = db.DeleteProcess(process);

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

        //POST : https://localhost:44391/api/Process/UsingProcess
        [HttpPost]
        [Route("UsingProcess")]
        public IHttpActionResult UsingProcess(ProcessVO process)
        {
            try
            {
                ProcessDAC db = new ProcessDAC();
                bool flag = db.UsingProcess(process);

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
        /// 등록된 모든 공정를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/Process/GetEquipName
        [Route("GetEquipName")]
        public IHttpActionResult GetEquipName()
        {
            try
            {
                ProcessDAC db = new ProcessDAC();
                List<ComboItemVO> list = db.GetEquipName();

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

        //POST : https://localhost:44391/api/Process/SaveProcessEquip
        [HttpPost]
        [Route("SaveProcessEquip")]
        public IHttpActionResult SaveProcessEquip(List<EquipDetailsVO> equip)
        {
            try
            {
                /*if (equip == null || equip.Count < 1)
                {
                    throw new Exception("전달된 데이터가 없습니다.");
                }*/

                ProcessDAC db = new ProcessDAC();
                bool flag = db.SaveProcessEquip(equip);

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
        /// <summary>
        /// 등록된 모든 공정를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/Process/GetProcessEquip
        [Route("GetProcessEquip")]
        public IHttpActionResult GetProcessEquip()
        {
            try
            {
                ProcessDAC db = new ProcessDAC();
                List<EquipDetailsVO> list = db.GetProcessEquip();

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
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }

        }
    }
}
