using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class AuthResponseModel
    {
        public int Response { get; set; }
        public string Status { get; set; }
        public string Info { get; set; }
        public string Token { get; set; }
    }
}
