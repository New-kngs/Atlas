﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtlasMVCAPI.Models;
using AtlasDTO;
using System.Diagnostics;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/Item")]
    public class ItemController : ApiController
    {
        // Get : https://localhost:44391/api/Item
        [Route("AllItem")]
        public IHttpActionResult GetallItem()
        {
            try
            {
                ItemDAC db = new ItemDAC();
                List<ItemVO> list = db.GetAllItem();

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
                Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }


        // Get : https://localhost:44391/api/Item
        [Route("AllItemCategory")]
        public IHttpActionResult GetAllItemCategory()
        {
            try
            {
                ItemDAC db = new ItemDAC();
                List<ComboItemVO> list = db.GetAllItemCategory();

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
                Debug.WriteLine(err.Message);

                return Ok(new ResMessage()
                {
                    ErrCode = -9,
                    ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
                });
            }
        }


        // Get : https://localhost:44391/api/Item
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GeTItemById(string id)
        {
            try
            {
                ItemDAC db = new ItemDAC();
                ItemVO item = db.GeTItemById(id);

                ResMessage<ItemVO> result = new ResMessage<ItemVO>()
                {
                    ErrCode = (item == null) ? -9 : 0,
                    ErrMsg = (item == null) ? "조회중 오류발생" : "S",
                    Data = item
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
