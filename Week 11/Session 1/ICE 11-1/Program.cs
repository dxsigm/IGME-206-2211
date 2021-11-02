using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICE_11_1
{
    // implement the adjacency matrix and adjacency list for the graph in the Slack General channel for session 11-1
    class Program
    {
        static int[,] mGraph = new int[,]
        {
                     /* A */   /* B */   /* C */   /* D */ 
           /* A */ { -1,       -1,        1,       -1 },
           /* B */ { 1,        -1,        -1,      -1 },
           /* C */ { -1,       3,         -1,      8  },
           /* D */ { 22,       -1,        -1,      -1 }
        };

        // parallel adjacency lists
        // lGraph stores the connected nodes
        static int[][] lGraph = new int[][]
        {
            /* A */ new int[] { 2 },    /* C */
            /* B */ new int[] { 0 },    /* A */
            /* C */ new int[] { 1, 3 }, /* B, D */
            /* D */ new int[] { 0 },    /* A */
        };

        // wGraph stores the edge weights
        static int[][] wGraph = new int[][]
        {
            /* A */ new int[] { 1 },
            /* B */ new int[] { 1 },
            /* C */ new int[] { 3, 8 },
            /* D */ new int[] { 22 },
        };

        // more human readable SortedList indexed by the tuple of (source,destination)
        static SortedList<(string, string), int> slMatrix = new SortedList<(string, string), int>();

        static void Main(string[] args)
        {
            slMatrix[("A", "A")] = -1;
            slMatrix[("A", "B")] = -1;
            slMatrix[("A", "C")] = 1;
            slMatrix[("A", "D")] = -1;

            slMatrix[("B", "A")] = 1;
            slMatrix[("B", "B")] = -1;
            slMatrix[("B", "C")] = -1;
            slMatrix[("B", "D")] = -1;

            slMatrix[("C", "A")] = -1;
            slMatrix[("C", "B")] = 3;
            slMatrix[("C", "C")] = -1;
            slMatrix[("C", "D")] = 8;

            slMatrix[("D", "A")] = 22;
            slMatrix[("D", "B")] = -1;
            slMatrix[("D", "C")] = -1;
            slMatrix[("D", "D")] = -1;
        }
    }
}
