using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TVDBapi
{
    [DataContract]
    class Token
    {
        [DataMember(Name = "token")]
        public string token { get; set; }
                
        public string apiKey { get; set; }
    }
}
