using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/Plan")]
    public class PlanController : ApiController
    {
        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>주문리스트 중 작업시지 되지 않은 리스트를 조회해서 반환</returns>
        //https://localhost:44391/api/Plan/ReadyList/{from}/{to}
        [Route("ReadyList/{from}/{to}")]
        public IHttpActionResult GetReadyList(string from, string to)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                List<OrderVO> list = db.GetReadyList(from, to);

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
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>주문리스트의 상세내용을 조회해서 반환</returns>
        //https://localhost:44391/api/Plan/OrderDetail/{order}
        [Route("OrderDetail/{order}")]
        public IHttpActionResult GetOrderDetail(string order)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                List<PlanVO> list = db.GetOrderDetail(order);

                ResMessage<List<PlanVO>> result = new ResMessage<List<PlanVO>>()
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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>주문리스트 중 선택한 제품의 구성품을 조회해서 반환</returns>
        //https://localhost:44391/api/Plan/Components/{order}/{item}
        [Route("Components/{order}/{item}")]
        public IHttpActionResult GetComponents(string order, string item)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                List<PlanVO> list = db.GetComponents(order, item);

                ResMessage<List<PlanVO>> result = new ResMessage<List<PlanVO>>()
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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="plan"></param>
        /// <returns>주문리스트중 주문수량이 출하가능한 경우 LOT번호와 바코드 번호 생성해서 출고창고 입고 </returns>
        //POST : https://localhost:44391/api/Plan/SavePlanShip
        [HttpPost]
        [Route("SavePlanShip")]
        public IHttpActionResult SavePlanShip(PlanVO list)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                bool flag = db.SavePlanShip(list);

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
        /// Author : 정희록
        /// </summary>
        /// <returns>주문리스트의 상세내용을 조회해서 반환</returns>
        //https://localhost:44391/api/Plan/LOTlist/{order}
        [Route("LOTlist/{order}")]
        public IHttpActionResult GetLOTList(string order)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                List<PlanVO> list = db.GetLOTList(order);

                ResMessage<List<PlanVO>> result = new ResMessage<List<PlanVO>>()
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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //POST : https://localhost:44391/api/Plan/SavePlanAdd
        [HttpPost]
        [Route("SavePlanAdd")]
        public IHttpActionResult SavePlanAdd(PlanVO list)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                bool flag = db.SavePlanAdd(list);

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
        /// Author : 정희록
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //POST : https://localhost:44391/api/Plan/SavePlanPlan
        [HttpPost]
        [Route("SavePlanPlan")]
        public IHttpActionResult SavePlanPlan(PlanOptVO list)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                bool flag = db.SavePlanPlan(list);

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
        /// Author : 류경석
        /// </summary>
        /// <returns>생산계획리스트 가져오기</returns>
        //https://localhost:44391/api/Plan/GetPlanList
        [Route("GetPlanList")]
        public IHttpActionResult GetPlanList()
        {
            try
            {
                PlanDAC db = new PlanDAC();
                List<PlanVO> list = db.GetPlanList();

                ResMessage<List<PlanVO>> result = new ResMessage<List<PlanVO>>()
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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //POST : https://localhost:44391/api/Plan/SaveOperation
        [HttpPost]
        [Route("SaveOperation")]
        public IHttpActionResult SaveOperation(OperationVO oper)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                bool flag = db.SaveOperation(oper);

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
