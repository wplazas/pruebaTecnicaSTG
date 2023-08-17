using Microsoft.AspNetCore.Mvc;

namespace pruebaTecnicaSTG.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public List<User> DB()
        {
            var list = new List<User>()
            {
                new User
                {
                    id=1,
                    username="testwill",
                    password = "passwill"
                }

            };
            return list;
        }
         

    }
}
