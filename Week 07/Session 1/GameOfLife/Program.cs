//#define EXE_BUILD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameOfLife
{
    public enum EAliveState
    {
        alive,
        dead
    }

    public enum EDirection
    {
        right,
        down,
        left,
        up,
        topLeft,
        topRight,
        bottomLeft,
        bottomRight
    }

    public struct StructCellState
    {
        public EAliveState eAliveState;
    }

    static public class Game
    {
        public static bool bExit = false;
        public static Random rand = new Random();

        public static int MAX_ROWS = 40;
        public static int MAX_COLS = 80;

        public static Cell[,] organism;

        public class Cell
        {
            public static int MAX_NEIGHBORS = Enum.GetNames(typeof(EDirection)).Length;

            public Cell[] neighbor; //  = new Cell[MAX_NEIGHBORS];

            public Cell nextCell;

            public object gameObject;

            public StructCellState prevCellState;
            public StructCellState currentCellState;
            public StructCellState nextCellState;

            public Cell(int maxCells, int probability = 6)
            {
                neighbor = new Cell[MAX_NEIGHBORS];

                probability = rand.Next(0, probability);

                if( probability == 0)
                {
                    currentCellState.eAliveState = EAliveState.alive;
                }
                else
                {
                    currentCellState.eAliveState = EAliveState.dead;
                }
            }

            public void SetNextState()
            {
                int nAlive = 0;

                for(int nCntr = 0; nCntr < MAX_NEIGHBORS; ++nCntr)
                {
                    Cell neighborCell = neighbor[nCntr];
                    if( neighborCell != null)
                    {
                        if( neighborCell.currentCellState.eAliveState == EAliveState.alive)
                        {
                            ++nAlive;
                        }
                    }
                }

                nextCellState = currentCellState;

                if( nAlive < 2 || nAlive > 3)
                {
                    nextCellState.eAliveState = EAliveState.dead;
                }
                else
                {
                    if( nAlive == 3)
                    {
                        nextCellState.eAliveState = EAliveState.alive;
                    }
                }
            }
        }


#if EXE_BUILD
        static void Main(string[] args)
#else
        static public void CreateOrganism(int probability)
#endif
        {
            string[] sInitialState = new string[MAX_ROWS];
            int nIniRows = 0;

#if EXE_BUILD
            Console.WriteLine("Enter initial state of the organism (1=alive,sp(0)=dead): ");

            do
            {
                sInitialState[nIniRows] = Console.ReadLine();
                if (sInitialState[nIniRows].Length == 0)
                {
                    break;
                }
            
                ++nIniRows;
            } while (nIniRows < MAX_ROWS);

            int probability = 6;
#endif

            // Gosper glider gun
            sInitialState[0] = "                         1";
            sInitialState[1] = "                       1 1";
            sInitialState[2] = "             11      11            11";
            sInitialState[3] = "            1   1    11            11";
            sInitialState[4] = " 11        1     1   11";
            sInitialState[5] = " 11        1   1 11    1 1";
            sInitialState[6] = "           1     1       1";
            sInitialState[7] = "            1   1";
            sInitialState[8] = "             11";
            nIniRows = 9;

            // Simkin glider gun
            //sInitialState[0] = "           ";
            //sInitialState[1] = "           ";
            //sInitialState[2] = "           ";
            //sInitialState[3] = "           ";
            //sInitialState[4] = "           ";
            //sInitialState[5] = "           ";
            //sInitialState[6] = "           ";
            //sInitialState[7] = "           ";
            //sInitialState[8] = "           ";
            //sInitialState[9] = "           ";
            //sInitialState[10] = "           ";
            //sInitialState[11] = "           ";
            //sInitialState[12] = "           ";
            //sInitialState[13] = "           ";
            //sInitialState[14] =  "           ";
            //sInitialState[15] =  "           ";
            //sInitialState[16] =  "           ";
            //sInitialState[17] =  "           ";
            //sInitialState[18] =  "    11     11";
            //sInitialState[19] =  "    11     11";
            //sInitialState[20] =  "           ";
            //sInitialState[21] =  "        11   ";
            //sInitialState[22] =  "        11   ";
            //sInitialState[23] =  "           ";
            //sInitialState[24] =  "           ";
            //sInitialState[25] =  "           ";
            //sInitialState[26] = "           ";
            //sInitialState[27] = "                          11 11      ";
            //sInitialState[28] = "                         1     1         ";
            //sInitialState[29] = "                         1      1  11    ";
            //sInitialState[30] = "                         111   1   11";
            //sInitialState[31] = "                              1          ";
            //sInitialState[32] = "                                   ";
            //sInitialState[33] = "                                   ";
            //sInitialState[34] = "                                   ";
            //sInitialState[35] = "                        11           ";
            //sInitialState[36] = "                        1                ";
            //sInitialState[37] = "                         111             ";
            //sInitialState[38] = "                           1             ";
            //nIniRows = 39;


            organism = new Cell[MAX_ROWS, MAX_COLS];
            for( int row = 0; row < MAX_ROWS; ++row)
            {
                for(int col = 0; col < MAX_COLS; ++col)
                {
                    organism[row, col] = new Cell(MAX_ROWS * MAX_COLS, probability);

                    if( nIniRows > 0)
                    {
                        if( row < nIniRows)
                        {
                            if( col < sInitialState[row].Length)
                            {
                                if( sInitialState[row][col] == '1')
                                {
                                    organism[row, col].currentCellState.eAliveState = EAliveState.alive;
                                }
                                else
                                {
                                    organism[row, col].currentCellState.eAliveState = EAliveState.dead;
                                }
                            }
                            else
                            {
                                organism[row, col].currentCellState.eAliveState = EAliveState.dead;
                            }
                        }
                        else
                        {
                            organism[row, col].currentCellState.eAliveState = EAliveState.dead;
                        }
                    }
                }
            }

            for (int row = 0; row < MAX_ROWS; ++row)
            {
                for (int col = 0; col < MAX_COLS; ++col)
                {
                    Cell thisCell = organism[row, col];
                    Cell nextCell = null;

                    for( int nCntr = 0; nCntr < Cell.MAX_NEIGHBORS; ++nCntr)
                    {
                        Cell neighborCell = null;

                        switch(nCntr)
                        {
                            case (int)EDirection.right:
                                if(col < MAX_COLS - 1)
                                {
                                    neighborCell = organism[row, col + 1];
                                    nextCell = organism[row, col + 1];
                                }
                                else if(row < MAX_ROWS - 1)
                                {
                                    nextCell = organism[row + 1, 0];
                                }
                                break;

                            case (int)EDirection.down:
                                // bottom neighbor = (row + 1, col)
                                if (row < MAX_ROWS - 1)
                                {
                                    neighborCell = organism[row + 1, col];
                                }
                                break;

                            case (int)EDirection.left:
                                // left neighbor = (row, col - 1)
                                if (col > 0)
                                {
                                    neighborCell = organism[row, col - 1];
                                }
                                break;

                            case (int)EDirection.up:
                                // top neighbor = (row - 1, col)
                                if (row > 0)
                                {
                                    neighborCell = organism[row - 1, col];
                                }
                                break;

                            case (int)EDirection.topRight:
                                // topright neighbor = (row - 1, col + 1)
                                if (row > 0 && col < MAX_COLS - 1)
                                {
                                    neighborCell = organism[row - 1, col + 1];
                                }

                                break;

                            case (int)EDirection.bottomRight:
                                // bottomright neighbor = (row + 1, col + 1)
                                if (row < MAX_ROWS - 1 && col < MAX_COLS - 1)
                                {
                                    neighborCell = organism[row + 1, col + 1];
                                }
                                break;

                            case (int)EDirection.topLeft:
                                // topleft neighbor = (row - 1, col - 1)
                                if (col > 0 && row > 0)
                                {
                                    neighborCell = organism[row - 1, col - 1];
                                }
                                break;

                            case (int)EDirection.bottomLeft:
                                // bottomleft neighbor = (row + 1, col - 1)
                                if (row < MAX_ROWS - 1 && col > 0)
                                {
                                    neighborCell = organism[row + 1, col - 1];
                                }
                                break;
                        }

                        thisCell.neighbor[nCntr] = neighborCell;
                    }

                    thisCell.nextCell = nextCell;                    
                }
            }

            // since we will use Unicode characters, set the console to display Unicode
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            // assign a delegate method to handle when CTRL+C is pressed
            // in order to exit our infinite game loop
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ConsoleCancel);

            while(!bExit)
            {
                // print the current state of the organism
                PrintOrganism(organism, MAX_ROWS, MAX_COLS);

                // calculate the next state of every cell based on the current state of every cell
                CalculateNextGeneration(organism[0, 0]);

                // sleep for 100 milliseconds
                Thread.Sleep(100);
            }

        }


        public static void ConsoleCancel(object sender, ConsoleCancelEventArgs e)
        {
            // CTRL+C sets bExit = true
            bExit = true;
        }

        // recursive method to calculate the next state of every cell based on the current state of all neighbors
        public static void CalculateNextGeneration(Cell thisCell)
        {
            // base case is if we reached thisCell.nextCell == null
            if (thisCell != null)
            {
                thisCell.prevCellState = thisCell.currentCellState;

                // calculate the next state for the current cell
                thisCell.SetNextState();

                // recurse through the whole linked list of thisCell.nextCell (moves through all cells to the "right")
                CalculateNextGeneration(thisCell.nextCell);

                // after all next states have been calculated for the organism
                // unfold the calling stack for every cell
                // and set the current state = the next state 
                // (these are structures, so they can be copied by value)
                thisCell.currentCellState = thisCell.nextCellState;
            }
        }


        public static void PrintOrganism(Cell[,] organism, int maxRows, int maxCols)
        {
            string output = "----------------------------------------------------------------------------------\n";

            for (int row = 0; row < maxRows; ++row)
            {
                output += "|";
                for (int col = 0; col < maxCols; ++col)
                {
                    Cell thisCell = organism[row, col];
                    if (thisCell.currentCellState.eAliveState == EAliveState.alive)
                    {
                        output += "\x25cb";
                    }
                    else
                    {
                        output += " ";
                    }
                }

                output += "|\n";
            }

            output += "----------------------------------------------------------------------------------";

            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(output);
        }
    }
}
