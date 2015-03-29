using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitMQ.Client;
using System.Text;

namespace MVCWriteEvents
{
    public class RabbitMessageSender : IMessageSender
    {
        public RabbitMessageSender()
        {

        }
        public bool Send(string Topic, string Payload)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(Topic, false, false, false, null);

                    var body = Encoding.UTF8.GetBytes(Payload);

                    channel.BasicPublish("", Topic, null, body);
                    Console.WriteLine(" [x] Sent {0}", Payload);
                }
            }


            return true;
        }
    }
}