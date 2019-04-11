using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class LibraryItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string PublishYear { get; set; }
        public int Type { get; set; }
        public string Access { get; set; }
    }
}
