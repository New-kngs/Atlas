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
    [RoutePrefix("api/Equipment")]
    public class EquipmentController : ApiController
    {
        /// <summary>
        /// 등록된 모든 설비를 조회해서 반환
        /// </summary>
        //https://localhost:44391/api/Process/AllEquipment
        [Route("AllEquipment")]
        public IHttpActionResult GetAllEquipment()
        {
            try
            {
                EquipmentDAC db = new EquipmentDAC();
                List<EquipmentVO> list = db.GetAllEquipment();

                ResMessage<List<EquipmentVO>> result = new ResMessage<List<EquipmentVO>>()
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
