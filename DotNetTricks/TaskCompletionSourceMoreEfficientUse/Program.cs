using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCompletionSourceMoreEfficientUse
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
        
        static Task SleepAsyncA(int timeout)
        {
            return Task.Factory.StartNew(() => Thread.Sleep(timeout));
        }

        static Task SleepAsyncB(int timeout)
        {
            TaskCompletionSource<bool> tcs = null;
            var t = new System.Threading.Timer((data) => tcs.TrySetResult(true) , null, -1, -1);
            tcs = new TaskCompletionSource<bool>(t);
            t.Change(timeout, -1);

            return tcs.Task;
        }
    }
}
