using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ThreadingTests
{
    public class ThreadTest
    {
        private static object theLock = new object();
        private static long myCount = 0;

        void incrementMyCount()
        {
            for (int i = 0; i < 50000; i++)
            {
                // myCount++;

                // thread-safe atomic operation
                Interlocked.Increment(ref myCount);
            }
        }

        void decrementMyCount()
        {
            for (int i = 0; i < 50000; i++)
            {
                // myCount--;

                // thread-safe atomic operation
                Interlocked.Decrement(ref myCount);
            }
        }

        // a test of the most simple way of threading
        // using a race condition
        public void runRaceTest()
        {
            //Launch a thread
            Thread t1 = new Thread(incrementMyCount);
            Thread t2 = new Thread(decrementMyCount);

            t1.Start();
            t2.Start();

            // don't let main finish until all else is finished
            t1.Join();
            t2.Join();

            // now print out the value of count
            Console.WriteLine("Count is now: {0}", myCount);
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }


        public static void Main(string[] args)
        {
            ThreadTest test = new ThreadTest();
            test.runRaceTest();
        }
    }
}
