using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mybbs.Models
{
    public class Message
    {
   
        
            public string key;
            public string value;
            public Message(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
        
    }
}