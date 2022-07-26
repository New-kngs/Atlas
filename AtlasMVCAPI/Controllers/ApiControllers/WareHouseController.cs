﻿using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/WareHouse")]

    public class WareHouseController : ApiController
    {
        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>등록된 모든 창고를 조회해서 반환</returns>
        //https://localhost:44391/api/WareHouse/AllWareHouse
        [Route("AllWareHouse")]
        public IHttpActionResult GetAllWareHouse()
        {
            try
            {
                WareHouseDAC db = new WareHouseDAC();
                List<WareHouseVO> list = db.GetAllWareHouse();

                ResMessage<List<WareHouseVO>> result = new ResMessage<List<WareHouseVO>>()
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
        /// <param name="whid"></param>
        /// <returns>선택한 창고의 상세정보 가져오기 </returns>
        //https://localhost:44391/api/WareHouse/WareHouseInfo/wh_0001
        [HttpGet]
        [Route("WareHouseInfo/{whid}")]
        public IHttpActionResult GetWareHouseInfo(string whid)
        {
            try
            {
                WareHouseDAC db = new WareHouseDAC();
                List<ItemVO> list = db.GetWareHouseInfo(whid);

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
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="wareHouse"></param>
        /// <returns>새로운 창고 생성</returns>
        //POST : https://localhost:44391/api/WareHouse/SaveWareHouse
        [HttpPost]
        [Route("SaveWareHouse")]
        public IHttpActionResult SaveWareHouse(WareHouseVO wareHouse)
        {
            try
            {
                WareHouseDAC db = new WareHouseDAC();
                bool flag = db.SaveWareHouse(wareHouse);

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
        /// <param name="wareHouse"></param>
        /// <returns>선택한 창고를 미사용으로 처리</returns>
        //POST : https://localhost:44391/api/WareHouse/DeleteWareHouse
        [HttpPost]
        [Route("DeleteWareHouse")]
        public IHttpActionResult DeleteWareHouse(WareHouseVO wareHouse)
        {
            try
            {
                WareHouseDAC db = new WareHouseDAC();
                bool flag = db.DeleteWareHouse(wareHouse);

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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="wareHouse"></param>
        /// <returns>미사용중인 창고를 사용처리</returns>
        //POST : https://localhost:44391/api/WareHouse/UsingWareHouse
        [HttpPost]
        [Route("UsingWareHouse")]
        public IHttpActionResult UsingWareHouse(WareHouseVO wareHouse)
        {
            try
            {
                WareHouseDAC db = new WareHouseDAC();
                bool flag = db.UsingWareHouse(wareHouse);

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
        /// Author : 정희록
        /// </summary>
        /// <param name="wareHouse"></param>
        /// <returns>창고정보 수정</returns>
        //POST : https://localhost:44391/api/WareHouse/UpdateWareHouse
        [HttpPost]
        [Route("UpdateWareHouse")]
        public IHttpActionResult UpdateWareHouse(WareHouseVO wareHouse)
        {
            try
            {
                WareHouseDAC db = new WareHouseDAC();
                bool flag = db.UpdateWareHouse(wareHouse);

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
    }
}
