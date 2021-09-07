using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formula_arrays
{
    public struct ZFunction
    {
        public double dX;
        public double dY;
        public double dZ;

        // the constructor for this structure
        public ZFunction(double dX, double dY)
        {
            this.dX = dX;
            this.dY = dY;
            this.dZ = 2 * Math.Pow(dX, 3) + 3 * Math.Pow(dY, 3) + 6;
            this.dZ = Math.Round(this.dZ, 3);
        }
    }

    // Class: Examples
    // Author: David Schuh
    // Purpose: Contains examples for week #3
    // Restrictions: Only contains code snippets.  
    class Examples
    {
        // Method: Main
        // Purpose: Calculate a 3-d formula using rectangle array, jagged array and array of struct
        // Restrictions: None
        static void Main(string[] args)
        {
            {
                // 3-d formula rectangular array example

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
                for (x = -4; x <= 4; x += 0.1, ++nX)
                {
                    // round x to 1 decimal point because of rounding issues with double
                    x = Math.Round(x, 1);

                    // start with the 0'th "y" bucket for this value of x
                    nY = 0;

                    // loop through each value of y, increment nY after each loop
                    for (y = -2; y <= 5; y += 0.2, ++nY)
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

            {
                // 3-d formula jagged array example

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
                // jagged arrays can only be declared 1 dimension at a time
                // declare the array of 81 two dimensional arrays
                double[][][] zFunc = new double[81][][];

                for(nX = 0; nX < 81; ++nX )
                {
                    // declare the 36 one dimensional arrays for each value of x
                    zFunc[nX] = new double[36][];

                    for (nY = 0; nY < 36; ++nY)
                    {
                        // declare the single array of 3 values for each (x,y)
                        zFunc[nX][nY] = new double[3];
                    }
                }

                // loop through each value of x, increment the int nX after each loop
                for (x = -4; x <= 4; x += 0.1, ++nX)
                {
                    // round x to 1 decimal point because of rounding issues with double
                    x = Math.Round(x, 1);

                    // start with the 0'th "y" bucket for this value of x
                    nY = 0;

                    // loop through each value of y, increment nY after each loop
                    for (y = -2; y <= 5; y += 0.2, ++nY)
                    {
                        // round y to 1 decimal point because of rounding issues with double
                        y = Math.Round(y, 1);

                        // calculate z
                        z = 2 * Math.Pow(x, 3) + 3 * Math.Pow(y, 3) + 6;

                        // round z to 3 decimal places
                        z = Math.Round(z, 3);

                        // store x, y and z for this (x,y) data point
                        zFunc[nX][nY][0] = x;
                        zFunc[nX][nY][1] = y;
                        zFunc[nX][nY][2] = z;
                    }
                }
            }

            {
                // 3-d formula structure example

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


                ZFunction[] zArray = new ZFunction[81 * 36];

                int dataPointCntr = 0;

                // loop through each value of x, increment the int nX after each loop
                for (x = -4; x <= 4; x += 0.1, ++nX)
                {
                    // round x to 1 decimal point because of rounding issues with double
                    x = Math.Round(x, 1);

                    // start with the 0'th "y" bucket for this value of x
                    nY = 0;

                    // loop through each value of y, increment nY after each loop
                    for (y = -2; y <= 5; y += 0.2, ++nY)
                    {
                        // round y to 1 decimal point because of rounding issues with double
                        y = Math.Round(y, 1);

                        // create a new structure using the contructor to calculate x,y,z and round z to 3 decimals
                        ZFunction thisDataPoint = new ZFunction(x, y);
                        zArray[dataPointCntr++] = thisDataPoint;

                        // or we simply could have written
                        zArray[dataPointCntr++] = new ZFunction(x, y);
                    }
                }
            }
        }
    }
}
