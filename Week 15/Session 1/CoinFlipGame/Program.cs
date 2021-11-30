//#define USE_MATRIX
#define DIGRAPH

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CoinFlipGame
{
    static class Program
    {
        // if we represent each vertex as a 3 bit number, where H=1 and T=0, then there are 8 states of the coins
        // adjacency matrix representation

#if DIGRAPH
        // the adjacency values for the directed graph version.  
        // Only allow the paths that reach the goal
        static bool[,] mGraph = new bool[,]
        {
           { false   , true    , false   , false   , true    , false   , false   , false },
           { false   , false   , false   , true    , false   , false   , false   , false },
           { true    , false   , false   , false   , false   , false   , false   , false },
           { false   , false   , false   , false   , false   , false   , false   , true },
           { false   , false   , false   , false   , false   , false   , true    , false },
           { false   , false   , false   , false   , false   , false   , false   , false },
           { false   , false   , false   , false   , false   , false   , false   , true },
           { false   , false   , false   , false   , false   , true    , false   , false }
        };

        static int[][] lGraph = new int[][]
        {
            new int[] { 1, 4 },
            new int[] { 3 },
            new int[] { 0 },
            new int[] { 7 },
            new int[] { 6 },
            null,
            new int[] { 7 },
            new int[] { 5 }
        };

#else
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
#endif

        // We could also implement our adjacencies using many other data structures for example a SortedList based on tuples of the (source,target) nodes
        static SortedList<(string, string), bool> sGraph = new SortedList<(string, string), bool>();
        //sGraph[("TTT","TTT")] = false;

        static bool bWaitingForMove = false;

        static Object lockObject = new Object();

        static int nState = 5;
        static string sUserState;
        

        static void Main(string[] args)
        {
            Random rand = new Random();

            string sState;

            int nUserState;

            int nMoves = 0;

            while( nState == 5)
            {
                nState = rand.Next(0, 8);
            }

            Thread t = new Thread(DFS);
            t.Start();

            //DFS(nState);

            while( nState != 5)
            {
                sState = NState2SState(nState);

                Console.WriteLine("Current coin state: " + sState);

                Console.Write("Enter the desired state: ");
                //sUserState = Console.ReadLine();

                // use lock method for thread-safe code
                lock(lockObject)
                {
                    // anything in this code block which changes shared variables 
                    // is thread-safe, provided the child thread does the same
                    bWaitingForMove = true;
                }
                
                while (bWaitingForMove) ;

                Console.WriteLine(sUserState);

                nUserState = SState2NState(sUserState);

#if USE_MATRIX
                if( mGraph[nState,nUserState] == true)
                {
                    nState = nUserState;
                    ++nMoves;
                }
                else
                {
                    Console.WriteLine("That is an invalid move.");
                }
#else
                int[] thisStateList = lGraph[nState];
                bool valid = false;
                if( thisStateList != null)
                {
                    foreach(int n in thisStateList)
                    {
                        if( n == nUserState)
                        {
                            valid = true;
                            nState = nUserState;
                            ++nMoves;
                            break;
                        }
                    }
                }

                if( !valid)
                {
                    Console.WriteLine("That is an invalid move.");
                }
#endif
            }

            Console.WriteLine($"You won in {nMoves} moves!");
        }

        static int SState2NState(string sState)
        {
            int nState = 0;

            // HHT should convert to 6
            for(int i = 0; i < 3; ++i)
            {
                if(sState[i] == 'H')
                {
                    nState += (1 << (2 - i));
                }
            }

            return (nState);
        }

        static string NState2SState( int nState)
        {
            string r = null;

            // 6 should convert to HHT
            for(int i = 0; i < 3; ++i )
            {
                if ((nState & (1 << (2 - i))) != 0)
                {
                    r += "H";
                }
                else
                {
                    r += "T";
                }
            }

            return (r);
        }

        static void DFS()
        {
            bool[] visited = new bool[lGraph.Length];

            DFSUtil(nState, ref visited);
        }

        static void DFSUtil(int v, ref bool[] visited)
        {
            while (!bWaitingForMove) ;

            ///////////////////////////////////////////////
            // print out the DFS
            //visited[v] = true;
            //string sUserState = NState2SState(v);
            //Console.Write(sUserState + " ");
            //////////////////////////////////////////////

            int[] thisStateList = lGraph[v];

            if (thisStateList != null)
            {
                foreach (int n in thisStateList)
                {
                    if (!visited[n])
                    {
                        //////////////////////////////////////////
                        /// AI to choose next move
                        visited[n] = true;
                        sUserState = NState2SState(n);

                        // we need
                        lock (lockObject)
                        {
                            // thread-safe code to change shared variable
                            bWaitingForMove = false;
                        }
                        //////////////////////////////////////////

                        DFSUtil(n, ref visited);
                    }
                }
            }
        }
    }
}
