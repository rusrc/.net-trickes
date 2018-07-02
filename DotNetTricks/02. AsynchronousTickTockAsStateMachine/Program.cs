﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _02.AsynchronousTickTockAsStateMachine
{
    public class Program
    {
        static void Main(string[] args)
        {
            TickTockAsync();

            Console.ReadKey();
        }

        private static void TickTockAsync()
        {
            var stateMachine = new TickTockAsyncStateMachine();

            stateMachine.MoveNext();

        }

        public class TickTockAsyncStateMachine
        {
            private int state = 0;
            private TaskAwaiter awaiter;
            public void MoveNext()
            {
                switch (state)
                {
                    case 0: goto firstState;
                    case 1: goto secondState;
                    case 2: goto thirdState;
                }

            firstState:
                Console.WriteLine("Starting clock");
                goto secondAndHalfState;

            secondState:
               awaiter.GetResult();

            secondAndHalfState:
                Console.Write("Tick");
                awaiter = Task.Delay(500).GetAwaiter();
                if (!awaiter.IsCompleted)
                {
                    state = 2;
                    awaiter.OnCompleted(MoveNext);
                    return;
                }

            thirdState:
                awaiter.GetResult();
                Console.WriteLine("Tock");
                if (!awaiter.IsCompleted)
                {
                    state = 1;
                    awaiter.OnCompleted(MoveNext);
                    return;
                }
                goto secondState;
            }
        }
    }
}
