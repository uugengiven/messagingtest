using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCWriteEvents.Controllers
{
    public class ClientsController : ApiController
    {
        IMessageSender messages = new RabbitMessageSender(); // inject this at some point

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>
        public void Post([FromBody]PostModel model)
        {
            // Payload should be everything needed - the controller controls which message queue it hits
            messages.Send("adminportal.clientcreated", model.Payload);
        }

    }
}