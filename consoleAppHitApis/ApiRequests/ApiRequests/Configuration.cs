using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRequests
{

    class Configuration
    {
        public int DelayBetweenCalls { get; set; }
        public List<ApiCall> Calls { get; set; }

        public Configuration()
        {
            DelayBetweenCalls = 0;
            Calls = new List<ApiCall>();
        }
    }

    class ApiCall
    {
        public string Url { get; set; }
        public string Payload { get; set; }

        public ApiCall(string url, string payload)
        {
            Url = url;
            Payload = payload;
        }
    }
}
