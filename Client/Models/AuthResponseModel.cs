using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class AuthResponseModel
    {
        public int Response { get; set; }
        public string Status { get; set; }
        public string Info { get; set; }
        public int Value { get; set; }
        public string Token { get; set; }
    }
}
