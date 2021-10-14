using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfHanoi
{
    class Program
    {
        static Dictionary<string, Stack<int>> post = new Dictionary<string, Stack<int>>();

        static Queue<string[]> _autoMoves = new Queue<string[]>();

        static int nTurn;

        static void Main(string[] args)
        {
            int nDisks;

            post["A"] = new Stack<int>();
            post["B"] = new Stack<int>();
            post["C"] = new Stack<int>();

            Console.WriteLine("Move the disks from post A to post C. \nYou may not put a larger disk onto a smaller one.");
            Console.Write("Number of disks on post A: ");

            while (int.TryParse(Console.ReadLine(), out nDisks) == false) ;

            Console.Write("Autosolve (Y/N): ");
            string autoSolve = Console.ReadLine();


            post["A"].Push(nDisks + 1);
            post["B"].Push(nDisks + 1);
            post["C"].Push(nDisks + 1);

            int nCntr = nDisks;
            while( nCntr > 0)
            {
                post["A"].Push(nCntr);
                --nCntr;
            }

            string srcPost = null;
            string destPost = null;

            while( post["C"].Count != nDisks + 1)
            {
                PrintPosts(nDisks);

                tryAgain:

                if( autoSolve.ToLower().StartsWith("y"))
                {
                    GameSolver(nDisks, "A", "B", "C");

                    string[] sMove = _autoMoves.Dequeue();
                    srcPost = sMove[0];
                    destPost = sMove[1];
                }
                else
                {
                    Console.Write("Source Post: ");
                    srcPost = Console.ReadLine().ToUpper();

                    Console.Write("Destination Post: ");
                    destPost = Console.ReadLine().ToUpper();
                }

                if ( post[srcPost].Count == 1)
                {
                    Console.WriteLine("There are no disks on post " + srcPost);
                    goto tryAgain;
                }

                if( post[srcPost].Peek() > post[destPost].Peek())
                {
                    Console.WriteLine("You may not place a higher disk on a lower disk!");
                    goto tryAgain;
                }

                int nThisDisk = post[srcPost].Pop();
                post[destPost].Push(nThisDisk);
            }

            PrintPosts(nDisks);
        }

        static void PrintPosts(int nDisks)
        {
            List<int> aPost = new List<int>(post["A"].ToArray());
            List<int> bPost = new List<int>(post["B"].ToArray());
            List<int> cPost = new List<int>(post["C"].ToArray());

            aPost.Reverse();
            bPost.Reverse();
            cPost.Reverse();

            for (int i = nDisks; i > 0; --i)
            {
                Console.Write((aPost.Count > i ? aPost[i].ToString() : " "));
                Console.Write("   ");

                Console.Write((bPost.Count > i ? bPost[i].ToString() : " "));
                Console.Write("   ");

                Console.Write((cPost.Count > i ? cPost[i].ToString() : " "));
                Console.WriteLine();
            }

            Console.WriteLine("A   B   C");
            Console.WriteLine("Turn #" + nTurn);
            ++nTurn;
            Console.WriteLine();
        }

        static void GameSolver(int nDisks, string from, string spare, string to)
        {
            // base case
            if( nDisks == 1 )
            {
                string[] move = { from, to };
                _autoMoves.Enqueue(move);

                return;
            }

            // move the remaining disks from A to C using B as spare
            GameSolver(nDisks - 1, from, to, spare);

            string[] move1 = { from, to };
            _autoMoves.Enqueue(move1);

            // move the remaining disks from B to C using A as spare
            GameSolver(nDisks - 1, spare, from, to);
        }
    }
}
