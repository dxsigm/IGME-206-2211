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

        public static int MAX_ROWS = 30;
        public static int MAX_COLS = 80;

        public static Cell[,] organism;

        public class Cell
        {
            public static int MAX_NEIGHBORS = Enum.GetNames(typeof(EDirection)).Length;

            public Cell[] neighbor; //  = new Cell[MAX_NEIGHBORS];

            public Cell nextCell;

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


        static void Main(string[] args)
        {
            organism = new Cell[MAX_ROWS, MAX_COLS];
            for( int row = 0; row < MAX_ROWS; ++row)
            {
                for(int col = 0; col < MAX_COLS; ++col)
                {
                    organism[row, col] = new Cell(MAX_ROWS * MAX_COLS);
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
