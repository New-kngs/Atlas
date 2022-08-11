using AtlasDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AtlasMVCAPI.Models
{
    public class ShipDAC
    {

        string strConn;
        public ShipDAC()
        {
            strConn = WebConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public List<ShipVO> GetAllShip()
        {

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = new SqlConnection(strConn);
                    cmd.CommandText = @"SELECT O.OrderID as OrderID, C.CustomerName as CustomerName, L.BarCodeID as BarCodeID, convert(nvarchar(30), o.OrderShip, 120) as OrderShip,
                                    convert(nvarchar(30), o.CreateDate, 120) as CreateDate, O.CreateUser as CreateUser FROM TB_LOT AS L 
                                    INNER JOIN TB_Order AS O ON L.OrderID = O.OrderID
                                    INNER JOIN TB_Customer AS C ON O.CustomerID = C.CustomerID
                                    INNER JOIN TB_OrderDetails as D on O.OrderID = D.OrderID
                                    where O.OrderShip = 'N' 
                                    Group By L.BarCodeID , O.OrderID , C.CustomerName , O.OrderShip , O.CreateDate ,o.CreateUser";

                    cmd.Connection.Open();
                    List<ShipVO> list = Helper.DataReaderMapToList<ShipVO>(cmd.ExecuteReader());
                    cmd.Connection.Close();

                    if (list != null && list.Count > 0)
                        return list;
                    else
                        return null;
                }
            }

            catch(Exception err)
            {
                Console.WriteLine(err.Message);
                return null;

            }

        }


    }
}