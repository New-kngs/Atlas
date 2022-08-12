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
        //https://localhost:44391/api/Plan/Components/{item}
        [Route("Components/{item}")]
        public IHttpActionResult GetComponents(string item)
        {
            try
            {
                PlanDAC db = new PlanDAC();
                List<BOMVO> list = db.GetComponents(item);

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
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }
    }
}
