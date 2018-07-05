using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace _01.ProducerConsumerDataFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumerBlock = new ActionBlock<int>(new Action<int>(Consume));
            PrintThreadPoolUsage("Main");
            for (int i = 0; i < 5; i++)
            {
                consumerBlock.Post(i);
                Thread.Sleep(1000);
                PrintThreadPoolUsage("loop");
            }
            // Tell the block no more items will be coming
            consumerBlock.Complete();
            // wait for the block to shutdown
            consumerBlock.Completion.Wait();           
        }

        private static void Consume(int val)
        {
            PrintThreadPoolUsage("Consume");
            Console.WriteLine("{0}:{1} is thread pool thread {2}", Task.CurrentId, val,
            Thread.CurrentThread.IsThreadPoolThread);
        }

        private static void PrintThreadPoolUsage(string label)
        {
            int cpu;
            int io;
            ThreadPool.GetAvailableThreads(out cpu, out io);
            Console.WriteLine("{0}:CPU:{1},IO:{2}", label, cpu, io);
        }
    }
}
