using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWriteEvents
{
    public interface IMessageSender
    {
        bool Send(string Topic, string Payload);
    }
}