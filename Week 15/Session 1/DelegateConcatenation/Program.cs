using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp33
{
    class Program
    {

        delegate void IntView(int v);

        public static void Main()
        {
            IntView i, x, c, ic, all;

            // creating anonymous methods
            // these 3 definitions of i are synonymous
            i = delegate (int v) { Console.Write("'{0}' ", (char)v); };
            i = (int v) => { Console.Write("'{0}' ", (char)v); };
            i = (v) => { Console.Write("'{0}' ", (char)v); };

            x = delegate (int v) { Console.Write("0x{0:X} ", v); };
            c = delegate (int v) { Console.Write("{0} ", v); };

            Console.Write("\ni: "); i(32);
            Console.Write("\nx: "); x(32);
            Console.Write("\nc: "); c(32);

            // we can concatenate delegates so that when all() is called, i, x, and c are called
            all = i + x + c;
            Console.Write("\nall: "); all(32);

            // we can remove delegates by subtracting
            ic = all - x;
            Console.Write("\nic: "); ic(32);

            Console.WriteLine();
        }
    }
}
