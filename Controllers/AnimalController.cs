using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pruebaTecnicaSTG.Models;
using System.Data;
using System.Data.SqlClient;

namespace pruebaTecnicaSTG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly string conexion;

        public AnimalController(IConfiguration cadcon)
        {
            conexion = cadcon.GetConnectionString("mycon");
        }


        [HttpPost]
        [Route("animalSave")]
        [Authorize]

        public IActionResult animalSave([FromBody]Animals entity)
        {
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SP_AnimalInsert", conn);
                    cmd.Parameters.AddWithValue("name", entity.name);
                    cmd.Parameters.AddWithValue("breed", entity.breed);
                    cmd.Parameters.AddWithValue("birthDate", entity.birthDate);
                    cmd.Parameters.AddWithValue("sex", entity.sex);
                    cmd.Parameters.AddWithValue("price", entity.price);
                    cmd.Parameters.AddWithValue("status", entity.status);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                return StatusCode(statusCode: 200, new { mensaje = "ok" });


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { mensaje = ex.Message });

            }


        }

        [HttpPut]
        [Route("animalUpdate")]
        [Authorize]

        public IActionResult animalUpdate([FromBody] Animals entity)
        {
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SP_AnimalUpdate", conn);
                    cmd.Parameters.AddWithValue("animalId", entity.animalId);
                    cmd.Parameters.AddWithValue("name", entity.name);
                    cmd.Parameters.AddWithValue("breed", entity.breed);
                    cmd.Parameters.AddWithValue("birthDate", entity.birthDate);
                    cmd.Parameters.AddWithValue("sex", entity.sex);
                    cmd.Parameters.AddWithValue("price", entity.price);
                    cmd.Parameters.AddWithValue("status", entity.status);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                return StatusCode(statusCode: 200, new { mensaje = "ok" });


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { mensaje = ex.Message });

            }


        }


        [HttpDelete]
        [Route("animalDelete")]
        [Authorize]

        public IActionResult animalDelete(int animalId)
        {
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SP_AnimalDelete", conn);
                    cmd.Parameters.AddWithValue("animalId", animalId);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                return StatusCode(statusCode: 200, new { mensaje = "ok" });


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { mensaje = ex.Message });

            }


        }


        [HttpGet]
        [Route("animalFilter")]
        [Authorize]
        public IActionResult animalFilter(int animalId,string sex,string status)
        {
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SP_AnimalFilter", conn);
                    cmd.Parameters.AddWithValue("animalId", animalId);
                    cmd.Parameters.AddWithValue("sex", sex);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                return StatusCode(statusCode: 200, new { mensaje = "ok" });


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { mensaje = ex.Message });

            }


        }


        [HttpPost]
        [Route("purchase")]
        [Authorize]

        public IActionResult purchase(string purchaseOrder,int purchaseAnimalId, decimal purchaseAmout)
        {
            try
            {
                List<Purchase> list = new List<Purchase>();
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("sp_purchaseOrder", conn);
                    cmd.Parameters.AddWithValue("purchaseOrder", purchaseOrder);
                    cmd.Parameters.AddWithValue("purchaseAnimalId", purchaseAnimalId);
                    cmd.Parameters.AddWithValue("purchaseAmout", purchaseAmout);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Purchase()
                            {
                                name = reader.GetString("purchasename"),
                                breed = reader.GetString("purchasebreed"),
                                purchaseamount = reader.GetInt16("purchaseamout"),
                                price = reader.GetDecimal("purchasefreight"),
                                discount = reader.GetDecimal("purchasediscount"),
                                totalvalue = reader.GetDecimal("purchasetotalvalue")


                            });
                        }
                    }
                }
                return StatusCode(statusCode: 200, list);


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { mensaje = ex.Message });

            }


        }

        

    }
}
