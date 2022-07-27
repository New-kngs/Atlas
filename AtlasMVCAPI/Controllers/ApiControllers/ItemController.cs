using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AtlasMVCAPI.Models;
using AtlasDTO;
using System.Diagnostics;
using System.Web;
using System.IO;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/Item")]
    public class ItemController : ApiController
    {
        // Get : https://localhost:44391/api/Item/AllItem
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


        // Get : https://localhost:44391/api/Item/AllItemCategory
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


        // Get : https://localhost:44391/api/Item/{id}
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

        // SaveItem
        // POST : https://localhost:44391/api/Item/SaveItem
        [HttpPost]
        [Route("SaveItem")]
        public IHttpActionResult SaveItem()
        {
            try
            {                
                bool flag = false;

                ItemVO prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemVO>(HttpContext.Current.Request["Item"]);

                foreach (string file in HttpContext.Current.Request.Files)
                {
                    var postedFile = HttpContext.Current.Request.Files[file];
                    string uploadFileName = postedFile.FileName;                    

                    //1.서버에 업로드된 파일을 서버에 저장
                    string filePath = HttpContext.Current.Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    postedFile.SaveAs(filePath + uploadFileName);                        
                }

                //2.DB insert
                ItemDAC db = new ItemDAC();
                flag = db.SaveItem(prod);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "저장중 오류발생" : "S"
                };
                return Ok(result);

                ResMessage fileResult = new ResMessage()
                {
                    ErrCode = (flag) ? 0 : -9,
                    ErrMsg = (flag) ? "S" : "파일 저장 중 오류발생"
                };
                return Ok(fileResult);
                 
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


        // UpdateItem
        // POST : https://localhost:44391/api/Item/UpdateItem
        [HttpPost]
        [Route("UpdateItem")]
        public IHttpActionResult UpdateItem()
        {
            try
            {
                bool flag = false;

                ItemVO prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemVO>(HttpContext.Current.Request["Item"]);

                foreach (string file in HttpContext.Current.Request.Files)
                {
                    var postedFile = HttpContext.Current.Request.Files[file];
                    string uploadFileName = postedFile.FileName;                    

                    //1.서버에 업로드된 파일을 서버에 저장
                    string filePath = HttpContext.Current.Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    postedFile.SaveAs(filePath + uploadFileName);
                }

                ItemDAC db = new ItemDAC();
                flag = db.UpdateItem(prod);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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


        // DeleteItem
        // POST : https://localhost:44391/api/Item/DeleteItem
        [HttpPost]
        [Route("DeleteItem")]
        public IHttpActionResult DeleteItem(ItemVO item)
        {
            try
            {
                ItemDAC db = new ItemDAC();
                bool flag = db.DeleteItem(item);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "삭제 중 오류발생" : "S"
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

        // UsingItem
        // POST : https://localhost:44391/api/Item/UsingItem
        [HttpPost]
        [Route("UsingItem")]
        public IHttpActionResult UsingItem(ItemVO item)
        {
            try
            {
                ItemDAC db = new ItemDAC();
                bool flag = db.UsingItem(item);

                ResMessage result = new ResMessage()
                {
                    ErrCode = (!flag) ? -9 : 0,
                    ErrMsg = (!flag) ? "수정 중 오류발생" : "S"
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

    }
}
