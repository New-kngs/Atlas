﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtlasMVCAPI.Models;
using AtlasDTO;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/WareHouse")]

    public class WareHouseController : ApiController
    {
        /// <summary>
        /// 등록된 모든 창고를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/Employee/AllWareHouse
        [Route("AllWareHouse")]
        public IHttpActionResult GetAllEmployee()
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
    }
}
