using System;

namespace Shopping_App.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string acces_token { get; set; }
        public string error_description { get; set; }
        public DateTime expire_date { get; set; }
    }
}
