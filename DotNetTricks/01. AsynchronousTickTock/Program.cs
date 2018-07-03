using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.AsynchronousTickTock
{
    class Program
    {
        static void Main(string[] args)
        {
            TickTockAsync();

            //Console.ReadKey();
        }

        private static async void TickTockAsync()
        {
            Console.WriteLine("Starting Clock");
            while (true)
            {
                Console.Write("Tick ");
                await Task.Delay(500);
                Console.WriteLine("Tock");
                await Task.Delay(500);
            }
        }
    }
}
