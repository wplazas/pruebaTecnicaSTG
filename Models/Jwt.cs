using System.Security.Claims;

namespace pruebaTecnicaSTG.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }


        public static dynamic validateToken(ClaimsIdentity identiy)
        {
            try
            {
                if (identiy.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Invalid Key",
                        result = ""
                    };
                }

                var id = identiy.Claims.FirstOrDefault(x => x.Type == "id").Value;

                //User usr = new User();
                //usr = usr.DB().FirstOrDefault(x => x.id= id);

                return new
                {
                    sucess = true,
                    message = "Ok",
                    result = ""

                };


            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message,
                    result = ""
                };

            }

        }
    }
}
