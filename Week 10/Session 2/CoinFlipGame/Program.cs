using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinFlipGame
{
    class Program
    {
        // if we represent each vertex as a 3 bit number, where H=1 and T=0, then there are 8 states of the coins
        // adjacency matrix representation
        // the matrix shows whether you can pass from a row node to a column node
        // note that because HTH is the goal, it is not connected to any other node target
        static bool[,] mGraph = new bool[,]
        {            /* TTT */ /* TTH */ /* THT */ /* THH */ /* HTT */ /* HTH */ /* HHT */ /* HHH */
           /* TTT */ { false   , true    , false   , false   , true    , false   , false   , false },
           /* TTH */ { false   , false   , false   , true    , false   , false   , false   , false },
           /* THT */ { true    , false   , false   , false   , false   , false   , false   , false },
           /* THH */ { false   , false   , false   , false   , false   , false   , false   , true },
           /* HTT */ { false   , false   , false   , false   , false   , false   , true    , false },
           /* HTH */ { false   , false   , false   , false   , false   , false   , false   , false },
           /* HHT */ { false   , false   , false   , false   , false   , false   , false   , true },
           /* HHH */ { false   , false   , false   , false   , false   , true    , false   , false }
        };

        static int[][] lGraph = new int[][]
        {
            /* TTT */ new int[] { 1, 4 }, /* HHT, HTT */
            /* TTH */ new int[] { 3 },    /* THH */
            /* THT */ new int[] { 0 },    /* TTT */
            /* THH */ new int[] { 7 },    /* HHH */
            /* HTT */ new int[] { 6 },    /* HHT */
            /* HTH */ null,
            /* HHT */ new int[] { 7 },    /* HHH */
            /* HHH */ new int[] { 5 }     /* HTH */
        };

        // We could also implement our adjacencies using many other data structures for example a SortedList based on tuples of the (source,target) nodes
        SortedList<(string, string), bool> sGraph = new SortedList<(string, string), bool>();
        //sGraph[("TTT","TTT")] = false;

        static void Main(string[] args)
        {
        }
    }
}
