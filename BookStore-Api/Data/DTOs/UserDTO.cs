using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_Api.Data.DTOs
{
    public class UserDTO
    {


        public String UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

    }
}
