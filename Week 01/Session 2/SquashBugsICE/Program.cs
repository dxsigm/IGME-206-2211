using System;

namespace SquashBugs
{
    // Class: Program
    // Author: David Schuh
    // Purpose: Bug squashing exercise #1
    // Restrictions: None
    class Program
    {
        // Method: Main
        // Purpose: Prompt the user for a decimal-valued number
        //          Count down to 0 in 0.5 increments
        // Restrictions: None
        static void Main(string[] args)
        {
            // declare variable to hold user-entered number
            //Syntax error: Missing semicolon at the end of the statement
            //int myNum = 0
            //logic error: purpose asks for a dec number
            //int myNum = 0;
            float myNum = 0;


            // prompt for number entry
            //Console.Write(Enter a number:);
            //missing double quots (syntax error)
            Console.Write("Enter a number:");

            // read user input and convert to double
            // Convert.ToDouble(Console.ReadLine());
            // convert and store where? (Logical error)
            myNum = (float)Convert.ToDouble(Console.ReadLine());
            
            // output starting value
            // Syntax error: missing a + between end quote and myNum
            // Console.Write("starting value = " myNum);
            Console.Write("starting value = " + myNum);

            // while myNum is greater than 0
            // while (myNum < 0)
            // previous statement does the while loop while myNum is less than 0, not greater
            while (myNum > 0)
                (
                    // output explanation of calculation
                    Console.Write("{0} - 0.5 = ", myNum);

            // output the result of the calculation
            Console.WriteLine($"{myNum - 0.5}");
            )
        }
    }
}
