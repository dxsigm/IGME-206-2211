using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadStart
{
	public class CounterThread
	{
		public static void Run()
		{
			for (int i = 0; i < 1000; i++)
			{
				Console.WriteLine("Count:  " + i);
			}
		}

		public static void Main(string[] args)
		{
			Thread ct = new Thread(Run);
			Thread ct2 = new Thread(Run);
			Thread ct3 = new Thread(Run);
			Thread ct4 = new Thread(Run);
			Thread ct5 = new Thread(Run);

			Console.WriteLine("THis is before start.");
			ct.Start();
			ct2.Start();
			ct3.Start();
			ct4.Start();
			ct5.Start();

			ct.Join();

			Thread.Sleep(100);
			Console.WriteLine("THis is after start.");
			//ct.Suspend();

			Console.WriteLine("THis is the main program.");
			Console.ReadLine();
		}
	}
}
