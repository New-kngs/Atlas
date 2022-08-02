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
    [RoutePrefix("api/BOM")]

    public class BOMController : ApiController
    {
        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>등록된 BOM의 Item리스트를 조회해서 반환</returns>
        //https://localhost:44391/api/BOM/AllBOMItem
        [Route("AllBOMItem")]
        public IHttpActionResult GetBOMItemList()
        {
            try
            {
                BOMDAC db = new BOMDAC();
                List<BOMVO> list = db.GetBOMItemList();

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

        ///// <summary>
        ///// Author : 정희록
        ///// </summary>
        ///// <returns>등록된 BOM의 정전개를 조회해서 반환</returns>
        ////https://localhost:44391/api/BOM/BOMFoward
        //[Route("BOMFoward")]
        //public IHttpActionResult GetBOMForwardList()
        //{
        //    try
        //    {
        //        BOMDAC db = new BOMDAC();
        //        List<BOMVO> list = db.GetBOMForwardList();

        //        ResMessage<List<BOMVO>> result = new ResMessage<List<BOMVO>>()
        //        {
        //            ErrCode = (list == null) ? -9 : 0,
        //            ErrMsg = (list == null) ? "조회중 오류발생" : "S",
        //            Data = list
        //        };

        //        return Ok(result);
        //    }
        //    catch (Exception err)
        //    {
        //        System.Diagnostics.Debug.WriteLine(err.Message);

        //        return Ok(new ResMessage()
        //        {
        //            ErrCode = -9,
        //            ErrMsg = "서비스 관리자에게 문의하시기 바랍니다."
        //        });
        //    }
        //}

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>BOM등록이 되지 않은 제품을 조회해서 반환</returns>
        //https://localhost:44391/api/BOM/UnregiItem
        [Route("UnregiItem")]
        public IHttpActionResult GetUnregiBOMList()
        {
            try
            {
                BOMDAC db = new BOMDAC();
                List<BOMVO> list = db.GetUnregiBOMList();

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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>BOM등록된 제품을 조회해서 반환</returns>
        //https://localhost:44391/api/BOM/RegiItem/반제품
        [Route("RegiItem/{category}")]
        public IHttpActionResult GetRegiBOMList(string category)
        {
            try
            {
                BOMDAC db = new BOMDAC();
                List<BOMVO> list = db.GetRegiBOMList(category);

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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>등록된 BOM의 완제품 정전개를 조회해서 반환</returns>
        //https://localhost:44391/api/BOM/BOMFoward/BD001
        [Route("BOMFoward/{itemID}")]
        public IHttpActionResult GetBOMForwardList(string itemID)
        {
            try
            {
                BOMDAC db = new BOMDAC();
                List<BOMVO> list = db.GetBOMForwardList(itemID);

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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <returns>등록된 BOM의 부품 역전개를 조회해서 반환</returns>
        //https://localhost:44391/api/BOM/BOMReward/BD001
        [Route("BOMReward/{itemID}")]
        public IHttpActionResult GetBOMRewardList(string itemID)
        {
            try
            {
                BOMDAC db = new BOMDAC();
                List<BOMVO> list = db.GetBOMRewardList(itemID);

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
