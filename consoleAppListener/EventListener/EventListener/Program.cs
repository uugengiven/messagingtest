using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;

namespace EventListener
{
    class Program
    {

        static void Listen(string QueueName, CancellationToken ct)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //channel.QueueDeclare(Topic, false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(QueueName, false, consumer);

                    Console.WriteLine(" [*] Waiting for messages." +
                                             "To exit press CTRL+C");
                    while (ct.IsCancellationRequested == false)
                    {
                        BasicDeliverEventArgs ea;
                        consumer.Queue.Dequeue(1000, out ea); // need a timeout to be able to kill the thread properly
                        if (ea != null)
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            Console.WriteLine(" [x] {1} Received {0}", message, QueueName);
                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            var ts = new CancellationTokenSource();
            CancellationToken ct = ts.Token;

            Task.Factory.StartNew(() => Listen("listener1", ct));
            Task.Factory.StartNew(() => Listen("listener2", ct));
            Task.Factory.StartNew(() => Listen("listener1", ct));
            Task.Factory.StartNew(() => Listen("listener3", ct));
            Task.Factory.StartNew(() => Listen("listener4", ct));
            Task.Factory.StartNew(() => Listen("listener5", ct));
            Task.Factory.StartNew(() => Listen("listener5", ct));
            Task.Factory.StartNew(() => Listen("listener5", ct)); 
            while (true)
            {
                var exit = Console.ReadLine();
                if (exit == "exit")
                {
                    break;
                }
            }

            ts.Cancel();
            Thread.Sleep(1200); // let the threads cancel
            ts.Dispose();
        }
    }
}
