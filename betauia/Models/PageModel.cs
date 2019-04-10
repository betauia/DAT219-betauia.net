using System.Collections.Generic;
using System.Net.Mime;

namespace betauia.Models
{
    public class PageModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        
        public string Content { get; set; }
    }
}