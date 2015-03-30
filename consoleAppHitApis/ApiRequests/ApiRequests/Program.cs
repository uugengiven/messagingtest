using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ApiRequests
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = setup();
            foreach (ApiCall call in config.Calls)
            {
                postToApi(call.Url, call.Payload);
                System.Threading.Thread.Sleep(config.DelayBetweenCalls * 1000);
            }

            while (true)
            {

                Console.WriteLine("Enter the url (exit to exit):");
                var url = Console.ReadLine();
                if (url == "exit") return;
                Console.WriteLine("Enter the body");
                var body = Console.ReadLine();

                postToApi(url, body);
            }
        }

        static Configuration setup()
        {
            Configuration _config = new Configuration();

            _config.DelayBetweenCalls = 1;
            _config.Calls.Add(new ApiCall("http://localhost:50123/api/clients", "payload=<xml><node>Some little bit of stuff</node></xml>"));
            _config.Calls.Add(new ApiCall("http://localhost:50123/api/clients", "payload=<xml><node>Some little bitanother call to see if it works</node></xml>"));
            _config.Calls.Add(new ApiCall("http://localhost:50123/api/CCS", "payload=adding a library"));
            _config.Calls.Add(new ApiCall("http://localhost:50123/api/clients", "payload=another call to see if it works"));
            _config.Calls.Add(new ApiCall("http://localhost:50123/api/CCS", "payload=<xml><node>another call to see if it works</node></xml>"));
            _config.Calls.Add(new ApiCall("http://localhost:50123/api/CCS", "payload=seeifineedtoavoidspaces"));
            _config.Calls.Add(new ApiCall("http://localhost:50123/api/clients", "payload=another call to see if it works"));

            return _config;
        }
        
        private static void postToApi(string url, string body)
        {
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(body);
            request.ContentLength = bytes.Length;

            System.IO.Stream outStream = request.GetRequestStream();
            outStream.Write(bytes, 0, bytes.Length);
            outStream.Close();

            Console.WriteLine("Sending: ");
            Console.WriteLine(body);

            System.Net.HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response == null)
            {
                Console.WriteLine("No response, null");
                return;
            }

            Console.WriteLine(response.StatusDescription);
        }

    }
}
