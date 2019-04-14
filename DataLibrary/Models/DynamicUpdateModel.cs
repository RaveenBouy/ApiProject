using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class DynamicUpdateModel
    {
        public string AuthToken { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
