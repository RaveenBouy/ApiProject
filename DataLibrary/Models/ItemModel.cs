using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }
        public string Category { get; set; }
        public string Access { get; set; }
        public string DateAdded { get; set; }
        public string DateUpdated { get; set; }
        public string AuthToken { get; set; }
    }
}
