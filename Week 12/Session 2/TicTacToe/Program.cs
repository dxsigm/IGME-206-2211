using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        const int MAX_SPACES = 9;
        const int MAX_WIN_STATES = 8;
        static int MAX_STATES = (int)Math.Pow(2, MAX_SPACES);

        // adjacency list (strength, next moves List<state>)
        static (int, List<int>)[] aList;

        static List<int>[] lWinStates = new List<int>[MAX_SPACES];
        static int[] strengths = new int[MAX_SPACES];
        static int[] winStates = new int[MAX_WIN_STATES];

        static Random random = new Random();

        static void Main(string[] args)
        {
            bool[] grid = new bool[MAX_SPACES];

            // p1 and p2 are the integer representations of the players game boards
            // using the lowest 9 bits to indicate their chosen spaces
            // bit 8 corresponds to the top left space of the game board
            // bit 7 corresponds to the top center space
            // ...
            // bit 0 (the least significant bit) corresponds to the bottom right space
            int p1 = 0;
            int p2 = 0;

            int nWinner = 0;
            int nPlayer = 1;

            int[] nValues = new int[]
            {
                2, 7, 6,
                9, 5, 1,
                4, 3, 8
            };

            int nMagicNumber = 15;

            int i;

            for(i = 0; i < MAX_SPACES; ++i )
            {
                lWinStates[i] = new List<int>();
            }

        }

        public static int GridToInt(bool[] g)
        {
            int r = 0;

            for (int i = 0; i < MAX_SPACES; ++i)
            {
                // bool[0] => bit 8
                if (g[i])
                {
                    r += (1 << ((MAX_SPACES - 1) - i));
                }
            }

            return (r);
        }

        public static bool[] IntToGrid(int c)
        {
            bool[] bCell = new bool[MAX_SPACES];

            for(int i = 0; i < MAX_SPACES; ++i)
            {
                // if this space's bit is set in the state passed in
                // 0th bit => "8"
                if (((1 << i) & c) != 0)
                {
                    // set the bool array value
                    // bCell[8] => "8"
                    bCell[(MAX_SPACES - 1) - i] = true;
                }
                else
                {
                    bCell[(MAX_SPACES - 1) - i] = false;
                }
            }

            return bCell;
        }

    }
}
