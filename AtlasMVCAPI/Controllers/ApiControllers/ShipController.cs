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
    [RoutePrefix("api/Ship")]
    public class ShipController : ApiController
    {


        // Get : https://localhost:44391/api/Ship/GetAllShip
        [Route("GetAllShip")]
        public IHttpActionResult GetAllShip()
        {
            try
            {
                ShipDAC db = new ShipDAC();
                List<ShipVO> list = db.GetAllShip();

                ResMessage<List<ShipVO>> result = new ResMessage<List<ShipVO>>()
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
