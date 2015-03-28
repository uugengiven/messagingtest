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
        MessageSender messages = new MessageSender();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]PostModel model)
        {
            // Payload should be everything needed - the controller controls which message queue it hits
            messages.Send("ClientCreated", model.Payload);
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}