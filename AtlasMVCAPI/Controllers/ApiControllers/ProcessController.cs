using AtlasDTO;
using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AtlasMVCAPI.Controllers
{
    [RoutePrefix("api/Process")]
    public class ProcessController : ApiController
    {
        /// <summary>
        /// 등록된 모든 사용자를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/Process/AllProcess
        [Route("AllProcess")]
        public IHttpActionResult GetAllProcess()
        {
            try
            {
                ProcessDAC db = new ProcessDAC();
                List<ProcessVO> list = db.GetAllProcess();

                ResMessage<List<ProcessVO>> result = new ResMessage<List<ProcessVO>>()
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
    }
}
