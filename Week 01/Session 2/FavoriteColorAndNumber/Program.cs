using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteColorAndNumber
{
    // Class: Program
    // Author: David Schuh
    // Purpose: Console Read/Write and Exception-handling exercise
    // Restrictions: None
    static class Program
    {
        // Method: PrintMyColor
        // Purpose: Demonstrate passing variables "by value" and "by reference"
        // Restrictions: None
        // without the "ref" keyword, the variable is passed according to the default behavior
        // the primitive data types all behave as "by value" variables by default
        static void PrintMyColor(string sColorString)
        {
            // sColorString is a local string variable to this method
            // which will receive a copy of the passed variable from the calling method
            // append " is your favorite color" to sColorString, which will not change the variable in the calling method
            // note that a += b is the same as a = a + b
            sColorString += " is your favorite color";
            Console.WriteLine(sColorString);
        }

        // Method: PrintMyColorByReference
        // Purpose: Demonstrate passing variables "by value" and "by reference"
        // Restrictions: None
        // adding the "ref" keyword, the variable is passed "by reference" and the local
        // method variable sColorString will be a pointer or reference to the passed variable in the calling method
        static void PrintMyColorByReference(ref string sColorString)
        {
            // sColorString is a local string variable to this method
            // but it is a reference to the passed variable from the calling method
            // which means any changes to sColorString will change the variable in the calling method
            // append " is your favorite color" to sColorString, which WILL change the variable in the calling method
            // note that a += b is the same as a = a + b
            sColorString += " is your favorite color";
            Console.WriteLine(sColorString);
        }


        // Method: Main
        // Purpose: Prompt the user for their favorite color and number
        //          Output their favorite color (in limited text colors) their favorite number of times
        // Restrictions: None
        static void Main(string[] args)
        {
            // string to hold their favorite color
            // compile-time error: missing semi-colon
            //string color = null
            string sColor = null;
            string sNumber = null;

            string sDavesColor = null;
            string sTomsColor = null;

            // strings are "by value" data types so that changes to one do not affect the others
            sDavesColor = "red";
            sTomsColor = "blue";
            
            // even when we set one equal to another
            sDavesColor = sTomsColor;

            // sDavesColor does not change
            sTomsColor = "purple";


            {
                // variables only exist in the code block they are defined in
                int localInt = 0;
            }

            // localInt no longer exists here!


            // int to hold their favorite number
            int favNum = 0;

            // flag to indicate if they entered a valid number string
            bool bValid = false;

            // loop counter
            int i = 0;

            // prompt for favorite color
            // demonstrate including the tab special character.  
            //          "\n" is the newline character, which is added to the end of the string for the Console.WriteLine() method
            Console.Write("Enter your favorite color:\t");

            // read their favorite color from the keyboard
            // and store it in color
            // logic error
            // sNumber = Console.ReadLine();
            sColor = Console.ReadLine();

            // call PrintMyColor with the default by value/by reference behavior of sColor
            // sColor is a primitive data type (string) so by default it is "by value" and
            // a separate copy of it will be created in PrintMyColor() so that sColor will not be changed
            // by the method
            PrintMyColor(sColor);

            // force sColor to be passed by reference by using the "ref" keyword
            // the method variable will be a reference to sColor and any changes to the method
            // variable will change sColor (it is a shared variable because it was passed by reference)
            PrintMyColorByReference(ref sColor);

            // prompt for favorite number
            Console.Write("Enter your favorite number:\t");
            sNumber = Console.ReadLine();

            // this causes a run-time error with non-numeric string
            //favNum = Convert.ToInt32(sNumber);

            // while loop checks the condition before executing its code block
            while (!bValid)
            {
                try
                {
                    favNum = Convert.ToInt32(sNumber);
                    bValid = true;
                }
                catch
                {
                    // and "catch" any run-time exception that might occur from the "try" code block
                    // guide the user what kind of data we are expecting
                    Console.WriteLine("Please enter an integer.");

                    // flag that they have not entered valid data yet, so that we stay in the loop
                    bValid = false;
                }
            }

            // the do...while loop always executes its code block at least once
            //do
            //{
            //    try
            //    {
            //        favNum = Convert.ToInt32(sNumber);
            //        bValid = true;
            //    }
            //    catch
            //    {
            //        // and "catch" any run-time exception that might occur from the "try" code block
            //        // guide the user what kind of data we are expecting
            //        Console.WriteLine("Please enter an integer.");
            //
            //        // flag that they have not entered valid data yet, so that we stay in the loop
            //        bValid = false;
            //    }
            //} while (!bValid);

            // use a switch() statement to set the output text color for several favorite colors
            // the string class has a ToLower() method which allows us to more efficiently compare what the user entered
            // otherwise we would need to check for "red", "RED", "Red", "rEd", "reD", "REd", "rED" and "ReD"
            // note that color.ToLower() does not change the contents of color, but only returns a copy of color with all letters lower-cased
            switch (sColor.ToLower())
            {
                // set the text color to Red
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                // set the text color to Blue
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                // set the text color to Green
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                //  if none of the above cases are met, then invert the text color (black on white)
                default:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
            }

            // use a for(initialization;condition;operation) loop to output their favorite color the number of times as their favorite number
            // The three statements within the for() statement:
            //      1. initialization statement(s):  (i = 0) any variable initialization
            //          Note that this can include multiple comma-separated statements.
            //              For example:   for( i=0, y=0, color=Console.ReadLine(); i < favNum; ++i)
            //
            //      2. condition check:  (i < favNum) what to check at the beginning of the loop each time it loops (including the first time)
            //
            //      3. operation: (++i)  any operations to execute upon each subsequent start of the loop (not including the first time)
            //          Note that this can include multiple comma-separated statements.
            //              For example:   for( i=0, y=0, color=Console.ReadLine(); i < favNum; ++i, ++y)
            //
            //          Note that using the "continue" statement to return to the top of the loop will execute the operation statement(s)
            //          so that if you need to do something N times, you may have to --i before the "continue" to repeat the same value of i.
            //
            for (i = 0; i < favNum; ++i)
            {
                // using $"" causes string interpolation such that the {} within the string are compiled as discrete code blocks which can contain any expressions
                // in this case we add "!" to the color
                Console.WriteLine($"Your favorite color is {sColor + "!"}");

                // Two other ways to generate the same output:

                // 1. simple string concatenation (note that you do not use $ or {}):
                //      Console.WriteLine("Your favorite color is " + color + "!");

                // 2. string replacement using {} (but not $):
                //      Console.WriteLine("Your favorite color is {0}{1}", color, "!");
            }

            /* The above for() loop can be rewritten as a while() loop as follows:
            // initialize the counter outside of the loop
            i = 0;
            
            // while i < the number of times to write the output
            while( i < favNum )
            {
                // write the output
                Console.WriteLine($"Your favorite color is {color + "!"}");
            
                // increment the counter
                ++i;
            }  */


        }
    }
}
