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
                //System.Threading.Thread.Sleep(config.DelayBetweenCalls * 75);
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
            string baseUrl = "http://localhost:50123/api/";
            string[] types = new string[] {"clients", "CCS", "communications", "workflow"};
            string filename = "big.txt";
            int linespermessage = 8;
            int i = 0;
            int type = 0;
            Random rnd = new Random();


            Configuration _config = new Configuration();

            var file = new System.IO.StreamReader(filename);
            string lines;

            // set up about 3000 calls, randomly


            for (int j = 0; j < 3000; j++)
            {
                lines = "payload=some stuff";
                for (i = 0; i < linespermessage; i++ )
                {
                    lines += file.ReadLine();
                }
                type = rnd.Next(0, types.Length);

                _config.Calls.Add(new ApiCall(baseUrl + types[type], lines));

            }


            _config.DelayBetweenCalls = 30; //ms
            file.Close();
            file.Dispose();
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
