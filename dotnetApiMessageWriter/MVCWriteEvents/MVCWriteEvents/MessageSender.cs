using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWriteEvents
{
    public class MessageSender : IMessageSender
    {
        public MessageSender()
        {

        }
        public bool Send(string Topic, string Payload)
        {
            Console.WriteLine(Topic);
            Console.WriteLine(Payload);
            return false;
        }
    }
}