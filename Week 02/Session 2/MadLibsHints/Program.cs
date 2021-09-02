using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Madlibs
{
    class Program
    {
        static void Main(string[] args)
        {
            int numLibs = 0;
            int cntr = 0;
            int nChoice = 0;

            StreamReader input;

            // open the template file to count how many Mad Libs it contains
            input = new StreamReader("c:/templates/MadLibsTemplate.txt");
            

            // count how many lines are in the file (each line is a mad lib story)
            string line = null;
            while ((line = input.ReadLine()) != null)
            {
                ++numLibs;
            }

            // close it
            input.Close();

            // only allocate as many strings as there are Mad Lib stories
            string[] madLibs = new string[numLibs];

            // read the Mad Lib stories into the array of strings
            input = new StreamReader("c:\\templates\\MadLibsTemplate.txt");

            line = null;
            while ((line = input.ReadLine()) != null)
            {
                // set this array element to the current line of the template file
                madLibs[cntr] = line;

                // replace the "\\n" tag with the newline escape character
                madLibs[cntr] = madLibs[cntr].Replace("\\n", "\n");

                ++cntr;
            }

            input.Close();

            // prompt the user for which Mad Lib they want to play (between 1 to numLibs) and store it in nChoice
            // YOUR CODE HERE

            // split the Mad Lib into separate words
            string[] words = madLibs[nChoice].Split(' ');

            foreach (string word in words)
            {
                // if word is a placeholder
                // {
                //      prompt the user for the replacement
                //      and append the user response to the result string
                // }
                // else
                // {
                //      append word to the result string
                // }
            }

            // output the result string


            ///////////////////////////////////////////////////////
            // More About Arrays
            ///////////////////////////////////////////////////////
            {
                int[] myIntArray = { 5, 9, 10, 2, 99 };
            }

            {
                int[] myIntArray = new int[5] { 5, 9, 10, 2, 99 };
            }

            {
                int[] myIntArray = new int[5];

                myIntArray[0] = 5;
                myIntArray[1] = 9;
                myIntArray[2] = 10;
                myIntArray[3] = 2;
                myIntArray[4] = 99;
            }

            {
                int arraySize = 5;
                int[] myIntArray = new int[arraySize];
            }

            {
                // single dimension array useful for lists for 2d graphs
                int[] funcVal = new int[21];
                int x = 0;
                int xCntr = 0;
                int y = 0;

                // the value of y at x
                funcVal[x] = y;

                // we may want a parallel array to store each value of x
                int[] xArray = new int[21];

                // for example: y = 2 * x^2 + 3
                // fill the array for -10 <= x <= 10 (21 data points)
                for (x = -10; x <= 10; ++x, ++xCntr)
                {
                    // Math.Pow() returns a double, so we need to cast as int
                    y = 2 * (int)Math.Pow(x, 2) + 3;

                    // the array indexer must be a positive integer and 0-based
                    // (ie. we cannot store funcVal[-10])
                    funcVal[xCntr] = y;
                    xArray[xCntr] = x;
                }

                xCntr = 0;
                
                
                for (x = -10; x <= 10; ++x)
                {
                    xArray[xCntr++] = x;
                }

                /////////////////////////////////////////////////////////////////////////
                // or we may want to add a dimension to the array to store x as well
                int[,] funcVal2 = new int[21, 2];

                // for example: y = 2 * x^2 + 3
                // fill the array for -10 <= x <= 10 (21 data points)

                xCntr = 0;
                for (x = -10; x <= 10; ++x)
                {
                    y = 2 * (int)Math.Pow(x, 2) + 3;

                    // store x in the first element of the second dimension
                    funcVal2[xCntr, 0] = x;

                    // store y in the second element of the second dimension
                    funcVal2[xCntr, 1] = y;

                    ++xCntr;
                }
            }

            {
                // 2 dimension array useful for 3d graphs
                int[,] funcVal = new int[10, 10];
                int x = 0;
                int y = 0;
                int z = 0;

                // eg. the height at any location
                funcVal[x, y] = z;
            }

            {
                // 3 dimension array useful for 4-d data
                int[,,] funcVal = new int[10, 10, 10];
                int x = 0;
                int y = 0;
                int z = 0;
                int temperature = 0;

                // eg. the temperature at any location on earth
                funcVal[x, y, z] = temperature;
            }

            {
                // 4 dimension array useful for 5-d data
                int[,,,] funcVal = new int[10, 10, 10, 10];
                int planet = 0;
                int x = 0;
                int y = 0;
                int z = 0;
                int temperature = 0;

                // eg. the temperature at any location in solar system
                funcVal[planet, x, y, z] = temperature;
            }

            {
                double[,] hillHeight = { { 1, 2, 3, 4 }, { 2, 3, 4, 5 }, { 3, 4, 5, 6 } };
                foreach (double height in hillHeight)
                {
                    // notice the values are output [0,0], [0,1], [0,2], [0,3], [1,0], [1,1], [1,2], [1,3]
                    Console.WriteLine("{0}", height);
                }
            }

            {
                // rectangular array [,] versus jagged arrays

                // jagged array: an array of arrays!
                int[][] jaggedIntArray;

                jaggedIntArray = new int[2][];

                // first array element is an array of 3 values
                jaggedIntArray[0] = new int[3];

                // second array element is an array of 4 values
                jaggedIntArray[1] = new int[4];

                // set the 3 values of first el
                jaggedIntArray[0][0] = 1;
                jaggedIntArray[0][0] = 2;
                jaggedIntArray[0][0] = 3;

                // set the 4 values of the second el
                jaggedIntArray[1][0] = 1;
                jaggedIntArray[1][1] = 2;
                jaggedIntArray[1][2] = 3;
                jaggedIntArray[1][3] = 4;
            }

            {
                int[][] divisors1To10 = {   new    int[]   {1},
                                            new    int[]   {1,2},
                                            new    int[]   {1,3},
                                            new    int[]   {1,2,4},
                                            new    int[]   {1,5},
                                            new    int[]   {1,2,3, 6},
                                            new    int[]   {1,7},
                                            new    int[]   {1,2,4, 8},
                                            new    int[]   {1,3,9},
                                            new    int[]   {1,2,5,10}};

                foreach (int[] divisorsOfInt in divisors1To10)
                {
                    foreach (int divisor in divisorsOfInt)
                    {
                        Console.WriteLine(divisor);
                    }
                }
            }

            {
                // 3-d formula example

                // implement the code to calculate: z = 2x ^ 3 + 3y ^ 3 + 6
                // for -4 <= x <= 4 in 0.1 increments: there are 81 values of x
                // for -2 <= y <= 5 in 0.2 increments: there are 36 values of y

                // we need doubles to store all values of x, y, and z
                double x = 0;
                double y = 0;
                double z = 0;

                // we need ints to access the array dimensions for x and y
                int nX = 0;
                int nY = 0;

                // we declare our 3 dimensional array to hold:
                //        81 values of x
                //        36 values of y for each value of x
                //        3 values for each data point: the x, y and z
                double[,,] zFunc = new double[81, 36, 3];

                // loop through each value of x, increment the int nX after each loop
                for(x = -4; x <= 4; x += 0.1, ++nX )
                {
                    // round x to 1 decimal point because of rounding issues with double
                    x = Math.Round(x, 1);

                    // start with the 0'th "y" bucket for this value of x
                    nY = 0;

                    // loop through each value of y, increment nY after each loop
                    for( y = -2; y <= 5; y+= 0.2, ++nY )
                    {
                        // round y to 1 decimal point because of rounding issues with double
                        y = Math.Round(y, 1);

                        // calculate z
                        z = 2 * Math.Pow(x, 3) + 3 * Math.Pow(y, 3) + 6;

                        // round z to 3 decimal places
                        z = Math.Round(z, 3);

                        // store x, y and z for this (x,y) data point
                        zFunc[nX, nY, 0] = x;
                        zFunc[nX, nY, 1] = y;
                        zFunc[nX, nY, 2] = z;
                    }
                }
            }
        }
    }
}
