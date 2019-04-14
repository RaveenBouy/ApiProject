using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int UserType { get; set; }
        public bool IsVerified { get; set; }
        public string DateCreated { get; set; }
    }
}
