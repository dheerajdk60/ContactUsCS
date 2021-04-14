using System.Collections;

namespace ContactUs.Models
{
    public class Message 
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string message{ get; set; }
        public bool status{ get; set; }
        
    }
}