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
            //logic error: purpose asks for a dec numbery
            //int myNum = 0;
            float myNum = 0;
            
            // the byte data type stores the data in 1 byte or 8 bits
            byte bNumber;

            // int is Int32 and uses 32 bits to store the data
            int nNumber;

            nNumber = 5;
            
            // we need to explicitly convert a higher precision data type to store into a lower precision data type
            // explicit conversion by casting
            bNumber = (byte)nNumber;

            // explicit conversion by using the Convert class
            bNumber = Convert.ToByte(nNumber);

            // copying a lower precision data type into a higher precision data type can be done implicitly
            nNumber = bNumber;

            bool bValid = false;

            do
            {
                string sNumber = null;

                // prompt for number entry
                //Console.Write(Enter a number:);
                //missing double quots (syntax error)                
                Console.Write("Enter a number:");

                sNumber = Console.ReadLine();

                // TryParse() accepts the string and the variable to set as an "out" parameter
                // which is like "ref" except it is only used as output from the method
                // we cannot care what myNum was set to before calling the TryParse() method
                // it can only the OUTput from the method and cannot be relied on as input
                // TryParse() returns true or false for whether it was successful, thus
                // avoiding the need for try/catch
                bValid = float.TryParse(sNumber, out myNum);

                //try
                //{
                //    // read user input and convert to double
                //    // Convert.ToDouble(Console.ReadLine());
                //    // convert and store where? (Logical error)
                //
                //    // convert using the Convert class
                //    sNumber = Console.ReadLine();
                //    // note that there is not a Convert.ToFloat(),
                //    // therefore we must explicitly cast the ToDouble() to a float
                //    myNum = (float)Convert.ToDouble(sNumber);
                //
                //    // convert using the Parse method
                //    myNum = float.Parse(sNumber);
                //    // myInt = int.Parse(sNumber); 
                //
                //    bValid = true;
                //}
                //catch
                //{
                //    bValid = false;
                //}

            } while (!bValid);

            
            
            // output starting value
            // Syntax error: missing a + between end quote and myNum
            // Console.Write("starting value = " myNum);
            Console.Write("starting value = " + myNum);
            Console.WriteLine("");

            // while myNum is greater than 0
            // while (myNum < 0)
            // logical error: previous statement does the while loop while myNum is less than 0, not greater
            while (myNum > 0)
            {
                // output explanation of calculation
                Console.WriteLine("{0} - 0.5 = ", myNum);

                // output the result of the calculation
                Console.WriteLine($"{myNum - 0.5}");

                myNum = myNum - .5f;
                // same as myNum -= .5f; 
            }
        }
    }
}
