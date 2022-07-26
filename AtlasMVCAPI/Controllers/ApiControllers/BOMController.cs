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

        /// <summary>
        /// Author : 정희록
        /// </summary>
        /// <param name="list"></param>
        /// <returns>새로운 BOM 생성</returns>
        //https://localhost:44391/api/BOM/SaveBOM
        [HttpPost]
        [Route("SaveBOM")]
        public IHttpActionResult SaveBOM(List<List<BOMVO>> list)
        {
            try
            {
                if (list.Count < 2)
                    throw new Exception("등록 값 전달 오류");

                BOMDAC db = new BOMDAC();
                bool flag = db.SaveBOM(list[0], list[1]);

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
        /// <param name="itemid"></param>
        /// <returns>BOM삭제</returns>
        //https://localhost:44391/api/BOM/DeleteBOM
        [HttpGet]
        [Route("DeleteBOM/{itemid}")]
        public IHttpActionResult DeleteBOM(string itemid)
        {
            try
            {
                BOMDAC db = new BOMDAC();
                bool flag = db.DeleteBOM(itemid);

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
    }
}
