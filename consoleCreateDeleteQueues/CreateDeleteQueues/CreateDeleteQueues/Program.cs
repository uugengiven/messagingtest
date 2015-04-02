using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateDeleteQueues
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitQueueHandler handler = new RabbitQueueHandler();
            CreateExchange(handler);
            CreateQueues(handler);
        }

        static void CreateQueues(IQueueHandler handler)
        {
            List<string> topics = new List<string>();
            topics.Add("adminportal.*");
            handler.CreateQueue("listener1", topics);

            topics = new List<string>();
            topics.Add("*.librarycreated");
            handler.CreateQueue("listener2", topics);
        }

        static void DeleteQueues(IQueueHandler handler)
        {
            handler.DeleteQueue("SomeQueue");
        }

        static void CreateExchange(IQueueHandler handler)
        {
            handler.CreateExchange("default");
        }
    }
}
