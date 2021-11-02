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
        static bool[,] mGraph = new bool[,]
        {            /* TTT */ /* TTH */ /* THT */ /* THH */ /* HTT */ /* HTH */ /* HHT */ /* HHH */
           /* TTT */ { false   , true    , true    , false   , true    , false   , false   , false },
           /* TTH */ { true    , false   , false   , true    , false   , false   , false   , false },
           /* THT */ { true    , false   , false   , false   , false   , false   , false   , false },
           /* THH */ { false   , true    , false   , false   , false   , false   , false   , true },
           /* HTT */ { true    , false   , false   , false   , false   , false   , true    , false },
           /* HTH */ { false   , false   , false   , false   , false   , false   , false   , true  },
           /* HHT */ { false   , false   , false   , false   , true    , false   , false   , true },
           /* HHH */ { false   , false   , false   , true    , false   , true    , true    , false }
        };

        static int[][] lGraph = new int[][]
        {
            /* TTT */ new int[] { 1, 2, 4 }, /* TTH, THT, HTT */
            /* TTH */ new int[] { 0, 3 },    /* TTT, THH */
            /* THT */ new int[] { 0 },       /* TTT */
            /* THH */ new int[] { 1, 7 },    /* TTH, HHH */
            /* HTT */ new int[] { 0, 6 },    /* TTT, HHT */
            /* HTH */ new int[] { 7 },       /* HHH */
            /* HHT */ new int[] { 4, 7 },    /* HTT, HHH */
            /* HHH */ new int[] { 3, 5, 6 }  /* THH, HTH, HHT */
        };

        // We could also implement our adjacencies using many other data structures for example a SortedList based on tuples of the (source,target) nodes
        SortedList<(string, string), bool> sGraph = new SortedList<(string, string), bool>();
        //sGraph[("TTT","TTT")] = false;

        static void Main(string[] args)
        {
        }
    }
}
