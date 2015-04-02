using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateDeleteQueues
{
    public interface IQueueHandler
    {
        void CreateQueue(string name, List<string> topics);
        void DeleteQueue(string name);
        void CreateExchange(string name);
    }
}
