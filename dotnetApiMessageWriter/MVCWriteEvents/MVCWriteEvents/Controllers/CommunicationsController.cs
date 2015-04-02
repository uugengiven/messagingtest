using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCWriteEvents.Controllers
{
    public class CommunicationsController : ApiController
    {
        IMessageSender messages = new RabbitMessageSender(); // inject this at some point

        public void Post([FromBody]PostModel model)
        {
            // Payload should be everything needed - the controller controls which message queue it hits
            messages.Send("communications.EmailSent", model.Payload);
        }
    }
}
