using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ApiRequests
{
    [DataContract]
    class Response
    {
        [DataMember(Name="url")]
        public string url { get; set; }

        [DataMember(Name="payload")]
        public string payload { get; set; }

    }
}
