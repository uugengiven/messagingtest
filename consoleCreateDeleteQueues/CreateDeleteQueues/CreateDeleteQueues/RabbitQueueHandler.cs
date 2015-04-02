using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace CreateDeleteQueues
{
    
    class RabbitQueueHandler : IQueueHandler
    {

        public void CreateQueue(string name, List<string> topics)
        {
            // implement

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(name, true, false, false, null);
                    foreach (string topic in topics)
                    {
                        channel.QueueBind(name, "default", topic);
                    }

                }
            }
        }

        public void DeleteQueue(string name)
        {
            // implement
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeleteNoWait(name, false, false);
                }
            }
        }


        public void CreateExchange(string name)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(name, "topic");
                }
            }
        }
    }
}
