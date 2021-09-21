using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            string sNumber = null;
            int nNumber = 0;
            int nAnswer = 0;

            do
            {
                Console.Write("Enter a positive integer: ");
                sNumber = Console.ReadLine();
            } while (!int.TryParse(sNumber, out nNumber) && nNumber <= 0);

            // non-recursive solution (boring!)
            //nAnswer = 1;
            //
            //while( nNumber > 0)
            //{
            //    nAnswer *= nNumber;
            //    --nNumber;
            //}

            // N * (N-1) * (N-2)...
            //recursive solution (death-defying excitement!)
            nAnswer = Factorial(nNumber);
        }

        static int Factorial(int v)
        {
            int returnVal = 0;
            int nextVal = 0;

            // base case: factorial(0) = 1
            if( v == 0 )
            {
                returnVal = 1;
            }
            else if( v < 0 )
            {
                returnVal = -1;
            }
            else
            {
                // factorial of the next sequence
                nextVal = Factorial(v - 1);
                returnVal = v * nextVal;
            }

            return returnVal;
        }
    }
}
