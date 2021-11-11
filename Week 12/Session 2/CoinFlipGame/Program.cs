//#define USE_MATRIX
//#define DIGRAPH

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CoinFlipGame
{
    public class Node : IComparable<Node>
    {
        // any node-specific data here
        public int nState;

        public Node(int nState)
        {
            this.nState = nState;
            this.minCostToStart = int.MaxValue;
        }

        // fields needed for Dijkstra algorithm
        public List<Edge> edges = new List<Edge>();

        public int minCostToStart;
        public Node nearestToStart;
        public bool visited;

        public void AddEdge(int cost, Node connection)
        {
            Edge e = new Edge(cost, connection);
            edges.Add(e);
        }

        public int CompareTo(Node n)
        {
            return this.minCostToStart.CompareTo(n.minCostToStart);
        }
    }

    public class Edge : IComparable<Edge>
    {
        public int cost;
        public Node connectedNode;

        public Edge(int cost, Node connectedNode)
        {
            this.cost = cost;
            this.connectedNode = connectedNode;
        }
        public int CompareTo(Edge e)
        {
            return this.cost.CompareTo(e.cost);
        }

    }


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

        // parallel list of costs associated with the neighbors connected in lGraph
        static int[][] cGraph = new int[][]
{
            /* TTT */ new int[] { 1, 2, 4 }, /* TTH, THT, HTT */
            /* TTH */ new int[] { 1, 3 },    /* TTT, THH */
            /* THT */ new int[] { 2 },       /* TTT */
            /* THH */ new int[] { 3, 7 },    /* TTH, HHH */
            /* HTT */ new int[] { 4, 6 },    /* TTT, HHT */
            /* HTH */ new int[] { 7 },       /* HHH */
            /* HHT */ new int[] { 6, 7 },    /* HTT, HHH */
            /* HHH */ new int[] { 7, 7, 7 }  /* THH, HTH, HHT */
        };
#endif

        // We could also implement our adjacencies using many other data structures for example a SortedList based on tuples of the (source,target) nodes
        static SortedList<(string, string), bool> sGraph = new SortedList<(string, string), bool>();
        //sGraph[("TTT","TTT")] = false;

        static bool bWaitingForMove = false;

        static Object lockObject = new Object();

        static int nState = 5;
        static string sUserState;

        static List<Node> game = new List<Node>();

        static void Main(string[] args)
        {
            Random rand = new Random();

            string sState;

            int nUserState;

            int nMoves = 0;

            Node node;
            int i = 0;

            for ( i = 0; i < lGraph.Length; ++i )
            {
                node = new Node(i);
                game.Add(node);
            }
            
            for( i = 0; i < lGraph.Length; ++i)
            {
                int[] thisState = lGraph[i];
                int[] thisCosts = cGraph[i];
            
                for( int cCntr = 0; cCntr < thisState.Length; ++cCntr )
                {
                    game[i].AddEdge( thisCosts[cCntr], game[thisState[cCntr]]);

                    // logical error!  I was incrementing cCntr again
                    //++cCntr;
                }
            
                //game[i].edges.Sort();
            }

            List<Node> shortestPath = GetShortestPathDijkstra();

            while ( nState == 5)
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

        /****************************************************************************************
        The Dijkstra algorithm was discovered in 1959 by Edsger Dijkstra.
        This is how it works:
        
        1. From the start node, add all connected nodes to a queue.
        2. Sort the queue by lowest cost and make the first node the current node.
           For every child node, select the best node that leads to the shortest path to start.
           When all edges have been investigated from a node, that node is "Visited" 
           and you don´t need to go there again.
        3. Add each child node connected to the current node to the priority queue.
        4. Go to step 2 until the queue is empty.
        5. Recursively create a list of each node that leads to the shortest path 
           from end to start.
        6. Reverse the list and you have found the shortest path
        
        In other words, recursively for every child of a node, measure its distance to the start. 
        Store the distance and what node led to the shortest path to start. When you reach the end 
        node, recursively go back to the start the shortest way, reverse that list and you have the 
        shortest path.
        ******************************************************************************************/

        static public List<Node> GetShortestPathDijkstra()
        {
            DijstraSearch();
            List<Node> shortestPath = new List<Node>();
            shortestPath.Add(game[5]);
            BuildShortestPath(shortestPath, game[5]);
            shortestPath.Reverse();
            return (shortestPath);
        }

        static public void BuildShortestPath(List<Node> list, Node node)
        {
            if( node.nearestToStart == null)
            {
                return;
            }

            list.Add(node.nearestToStart);
            BuildShortestPath(list, node.nearestToStart);
        }

        static public void DijstraSearch()
        {
            Node start = game[2];

            start.minCostToStart = 0;
            List<Node> prioQueue = new List<Node>();

            prioQueue.Add(start);

            do
            {
                // sort method sorts the queue in place
                prioQueue.Sort();

                prioQueue = prioQueue.OrderBy(delegate (Node n) { return n.minCostToStart; }).ToList();
                prioQueue = prioQueue.OrderBy((Node n) => { return n.minCostToStart; }).ToList();
                prioQueue = prioQueue.OrderBy((n) => { return n.minCostToStart; }).ToList();
                prioQueue = prioQueue.OrderBy((n) => n.minCostToStart).ToList();
                prioQueue = prioQueue.OrderBy(n => n.minCostToStart).ToList();

                Node node = prioQueue.First();
                prioQueue.Remove(node);

                foreach (Edge cnn in node.edges.OrderBy(e => e.cost).ToList())
                {
                    Node childNode = cnn.connectedNode;
                    if (childNode.visited)
                    {
                        continue;
                    }

                    if (childNode.minCostToStart == int.MaxValue ||
                        node.minCostToStart + cnn.cost < childNode.minCostToStart)
                    {
                        childNode.minCostToStart = node.minCostToStart + cnn.cost;
                        childNode.nearestToStart = node;
                        if (!prioQueue.Contains(childNode))
                        {
                            prioQueue.Add(childNode);
                        }
                    }
                }

                node.visited = true;

                if (node == game[5])
                {
                    return;
                }
            } while (prioQueue.Any());
        }
    }
}
