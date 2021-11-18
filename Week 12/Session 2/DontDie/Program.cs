using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using System.IO;
using System.Timers;

namespace DontDie
{
    class Trivia
    {
        public int response_code;
        public List<TriviaResult> results;
    }

    class TriviaResult
    {
        public string category;
        public string type;
        public string difficulty;
        public string question;
        public string correct_answer;
        public List<string> incorrect_answers;
    }

    class Program
    {
        // relative direction is Item1 in the tuple
        // cost is Item2 in the tuple
        // (direction string, cost)
        static (string, int)[,] rectMatrixGraph = new (string, int)[,]
        {
                 //    A           B           C           D           E           F           G           H
          /*A*/  {("NE", 0),  ("S", 2),   (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1)},
          /*B*/  {(null, -1), (null, -1), ("S", 2),   ("E", 3),   (null, -1), (null, -1), (null, -1), (null, -1)},
          /*C*/  {(null, -1), ("N", 2),   (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), ("S", 20)},
          /*D*/  {(null, -1), ("W", 3),   ("S", 5),   (null, -1), ("N", 2),   ("E", 4),   (null, -1), (null, -1)},
          /*E*/  {(null, -1), (null, -1), (null, -1), (null, -1), (null, -1), ("S", 3),   (null, -1), (null, -1)},
          /*F*/  {(null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), ("E", 1),   (null, -1)},
          /*G*/  {(null, -1), (null, -1), (null, -1), (null, -1), ("N", 0),   (null, -1), (null, -1), ("S", 2)},
          /*H*/  {(null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1)}
        };


        static (string, int)[][] jaggedMatrixGraph = new (string, int)[][]
        {
                 //    A           B           C           D           E           F           G           H
          /*A*/  new (string,int)[] {("NE", 0),  ("S", 2),   (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1)},
          /*B*/  new (string,int)[] {(null, -1), (null, -1), ("S", 2),   ("E", 3),   (null, -1), (null, -1), (null, -1), (null, -1)},
          /*C*/  new (string,int)[] {(null, -1), ("N", 2),   (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), ("S", 20)},
          /*D*/  new (string,int)[] {(null, -1), ("W", 3),   ("S", 5),   (null, -1), ("N", 2),   ("E", 4),   (null, -1), (null, -1)},
          /*E*/  new (string,int)[] {(null, -1), (null, -1), (null, -1), (null, -1), (null, -1), ("S", 3),   (null, -1), (null, -1)},
          /*F*/  new (string,int)[] {(null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), ("E", 1),   (null, -1)},
          /*G*/  new (string,int)[] {(null, -1), (null, -1), (null, -1), (null, -1), ("N", 0),   (null, -1), (null, -1), ("S", 2)},
          /*H*/  new (string,int)[] {(null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1), (null, -1)}
        };

        // parallel matrices to implement the same as above
        // directional matrix
        static string[,] mDGraph = new string[,]
        {
                 //A     B    C     D     E      F     G       H
          /*A*/  {"NE", "S",  null, null,  null,  null, null,  null},
          /*B*/  {null, null, "S",  "E",   null,  null, null,  null},
          /*C*/  {null, "N",  null, null,  null,  null, null,  "S" },
          /*D*/  {null, "W",  "S",  null,  "N",   "E",  null,  null},
          /*E*/  {null, null, null, null,  null,  "S",  null,  null},
          /*F*/  {null, null, null, null,  null,  null, "E",   null},
          /*G*/  {null, null, null, null,  "N",   null, null,  "S" },
          /*H*/  {null, null, null, null,  null,  null, null,  null }
         };

        // cost matrix
        static int[,] mCGraph = new int[,]
        {
             //    A   B   C    D    E   F   G   H
          /*A*/  { 0,  2, -1,  -1,  -1, -1, -1, -1},
          /*B*/  {-1, -1,  2,   3,  -1, -1, -1, -1},
          /*C*/  {-1,  2, -1,  -1,  -1, -1, -1, 20},
          /*D*/  {-1,  3,  5,  -1,   2,  4, -1, -1},
          /*E*/  {-1, -1, -1,  -1,  -1,  3, -1, -1},
          /*F*/  {-1, -1, -1,  -1,  -1, -1,  1, -1},
          /*G*/  {-1, -1, -1,  -1,   0, -1, -1,  2},
          /*H*/  {-1, -1, -1,  -1,  -1, -1, -1, -1}
        };


        // storing all information in a tuple
        // Item1 is the index of the neighbor, Item2 is the direction and Item3 is the cost
        // (neighbor index, direction string, cost)
        static (int, string, int)[][] listGraph = new (int, string, int)[][]
        {
            /* listGraph[0] A*/ new (int, string, int)[] {(0, "N", 0), (0, "E", 0), (1, "S", 2)},
            /* listGraph[1] B*/ new (int, string, int)[] {(2, "S", 2), (3, "E", 3)},
            /* listGraph[2] C*/ new (int, string, int)[] {(1, "N", 2), (7, "S", 20)},
            /* listGraph[3] D*/ new (int, string, int)[] {(1, "W", 3), (2, "S", 5), (4, "N", 2), (5, "E", 4)},
            /* listGraph[4] E*/ new (int, string, int)[] {(5, "S", 3)},
            /* listGraph[5] F*/ new (int, string, int)[] {(6, "E", 1)},
            /* listGraph[6] G*/ new (int, string, int)[] {(4, "N", 0)},
            /* listGraph[7] H*/ null
        };

        // parallel arrays to store the room indexes, weight, and direction
        // room graphs: contains the indexes of the connected rooms
        static int[][] rGraph = new int[][]
        {
            /*A*/ new int[] { 0, 0, 1 },
            /*B*/ new int[] { 2, 3 },
            /*C*/ new int[] { 1, 7 },
            /*D*/ new int[] { 1, 2, 4, 5 },
            /*E*/ new int[] { 5 }, 
            /*F*/ new int[] { 6 }, 
            /*G*/ new int[] { 4 },
            /*H*/ null
        };


        // weight graph
        static int[][] wGraph = new int[][]
        {
            /*A*/ new int[] { 0, 0, 2 },
            /*B*/ new int[] { 2, 3 },
            /*C*/ new int[] { 2, 20 },
            /*D*/ new int[] { 3, 5, 2, 4 },
            /*E*/ new int[] { 3 },
            /*F*/ new int[] { 1 },
            /*G*/ new int[] { 0 },
            /*H*/ null
        };

        // direction graph
        static string[][] dListGraph = new string[][]
        {
            /*A*/ new string[] { "N", "E", "S" },
            /*B*/ new string[] { "S", "E" },
            /*C*/ new string[] { "N", "S" },
            /*D*/ new string[] { "W", "S", "N", "E" },
            /*E*/ new string[] { "S" },
            /*F*/ new string[] { "E" },
            /*G*/ new string[] { "N" },
            /*H*/ null
        };

        static Random rand = new Random();

        static Timer timer;
        static bool bTimedOut;

        static void Main(string[] args)
        {
            int nRoom = 0;

            int playerHp = 4;

            string[] desc = new string[]
            {
                "room A desc",
                "room B desc",
                "room C desc",
                "room D desc",
                "room E desc",
                "room F desc",
                "room G desc",
                "room H desc"
            };

            string[] randomInjuryDesc = new string[]
            {
                "you tripped",
                "you stubbed your toes",
                "you hit your head",
                "a bat bit you",
                "a rock fell on your head"
            };


            // nRoom keeps track of which room the player is in A=0, H=7
            // while not in room H (the exit)
            while (nRoom != 7)
            {

                // display a desc of the room
                Console.WriteLine(desc[nRoom]);

                // display any exits from the room
                // the neighbors of the current room are stored as an array of tuples
                // storing the neighbor index (0-7), the available direction (eg. "N"), and the cost of the exit
                // fetch the array of neighbors for the current room
                (int, string, int)[] thisRoomsNeighbors = listGraph[nRoom];

                int nExits = 0;

                // check each neighbor of the current room to see if it's available
                foreach ((int, string, int) neighbor in thisRoomsNeighbors)
                {
                    // if the player's HP is greater than the cost of the exit
                    if (playerHp > neighbor.Item3)
                    {
                        // display the exit
                        Console.WriteLine("There is an exit to the " + neighbor.Item2 + " which costs " + neighbor.Item3 + " HP");

                        // count how many exits are available
                        ++nExits;
                    }
                }

                // display the hp
                Console.WriteLine($"You have {playerHp} HP");

                // ask the player if they want wager (w) for more hp or leave (l) the room only if there are nExits > 0
                string sResponse = null;

                // if there are exits available
                if( nExits > 0 )
                {
                    while( sResponse != "l" && sResponse != "w")
                    {
                        // prompt for w or l
                        Console.Write("Would you like to wager for more health or leave the room? ");

                        // grab the first character and lowercase it
                        sResponse = Console.ReadLine().ToLower()[0].ToString();
                    }
                }
                else
                {
                    // they need to wager, there are no exits
                    sResponse = "w";
                }
                

                // if leaving room
                if (sResponse.ToLower() == "l" /* leaving room */ )
                {
                    // initialize that the direction is invalid
                    bool bValid = false;
                    string sDirection;

                    while (!bValid)
                    {
                        Console.Write("Which direction (N/S/E/W): ");

                        // read the first char of the direction
                        sDirection = Console.ReadLine().ToUpper()[0].ToString();

                        // check each neighbor of the current room
                        // (check each column of the current row of the adjacency matrix)
                        // array.GetLength(N) gets the size of the N'th dimension of the array
                        for (int nCntr = 0; nCntr < rectMatrixGraph.GetLength(1); ++nCntr)
                        {
                            // ensure the direction is valid
                            // the column is the room index (0-7), Item1 contains the valid directions, Item2 is the cost
                            if (rectMatrixGraph[nRoom, nCntr].Item1 != null && rectMatrixGraph[nRoom, nCntr].Item1.Contains(sDirection) && playerHp > rectMatrixGraph[nRoom, nCntr].Item2)
                            {
                                // indicate they chose a valid direction
                                bValid = true;

                                // deduct the HP
                                playerHp -= rectMatrixGraph[nRoom, nCntr].Item2;

                                // move to the room
                                nRoom = nCntr;

                                // if not room A (0) and not room H (7) then randomly reduce their HP such that they don't die
                                if (nRoom != 0 && nRoom != 7)
                                {
                                    Console.WriteLine(randomInjuryDesc[rand.Next(randomInjuryDesc.Length)]);

                                    int nHpLoss = rand.Next(playerHp);

                                    Console.WriteLine($"You lost {nHpLoss} health.");
                                    playerHp -= nHpLoss;
                                }

                                break;
                            }
                        }

                        // indicate invalid direction chosen
                        if (!bValid)
                        {
                            Console.WriteLine("That isn't a valid direction");
                        }
                    }
                }
                else
                {
                    // otherwise grinding for HP

                    // trivia question
                    // fetch api
                    // 15 second limit to answer
                    // multiple choice 1-4

                    // ask player how much HP to wager (limited to playerHp)

                    string url = null;
                    string s = null;

                    HttpWebRequest request;
                    HttpWebResponse response;
                    StreamReader reader;

                    url = "https://opentdb.com/api.php?amount=1&type=multiple";

                    request = (HttpWebRequest)WebRequest.Create(url);
                    response = (HttpWebResponse)request.GetResponse();
                    reader = new StreamReader(response.GetResponseStream());
                    s = reader.ReadToEnd();
                    reader.Close();

                    Trivia trivia = JsonConvert.DeserializeObject<Trivia>(s);

                    trivia.results[0].question = HttpUtility.HtmlDecode(trivia.results[0].question);
                    trivia.results[0].correct_answer = HttpUtility.HtmlDecode(trivia.results[0].correct_answer);
                    for (int i = 0; i < trivia.results[0].incorrect_answers.Count; ++i)
                    {
                        trivia.results[0].incorrect_answers[i] = HttpUtility.HtmlDecode(trivia.results[0].incorrect_answers[i]);
                    }

                    // prompt for wager amount
                    string sWager = null;
                    int nWager = 0;

                    do
                    {
                        Console.Write("Enter how much of your HP to wager: ");
                        sWager = Console.ReadLine();
                    } while( !int.TryParse(sWager, out nWager) || (nWager < 0) || (nWager > playerHp) );

                    // ask question
                    Console.WriteLine(trivia.results[0].question);

                    // choose random answer spot
                    int nAnswer = rand.Next(trivia.results[0].incorrect_answers.Count + 1);
                    int nWrongCntr = 0;

                    // output the correct answer in random position
                    // prefix each with 1-N to allow player to answer with N
                    for(int i = 0; i < trivia.results[0].incorrect_answers.Count + 1; ++i)
                    {
                        if( i == nAnswer )
                        {
                            // if this is the correct answer to show
                            Console.WriteLine($"{i + 1}: {trivia.results[0].correct_answer}");
                        }
                        else
                        {
                            // show the incorrect answers
                            Console.WriteLine($"{i + 1}: {trivia.results[0].incorrect_answers[nWrongCntr]}");
                            ++nWrongCntr;
                        }
                    }

                    // increment the answer to be 1-based instead of 0-based
                    ++nAnswer;

                    // 15 second timer
                    timer = new Timer(15000);

                    // use an anonymous method via a lambda expression to catch the lapsed timer event
                    timer.Elapsed += (o, e) => { bTimedOut = true; timer.Stop(); Console.WriteLine("Time's up. Press enter."); };

                    timer.Start();

                    Console.Write("==> ");
                    string sAnswer = Console.ReadLine();

                    timer.Stop();

                    sAnswer = nAnswer.ToString();

                    // if an incorrect answer
                    if ( sAnswer != nAnswer.ToString() || bTimedOut )
                    {
                        Console.WriteLine($"Wrong!  The answer was {nAnswer}.");
                        playerHp -= nWager;
                    }
                    else
                    {
                        Console.WriteLine("Correct! You are stronger!");
                        playerHp += nWager;
                    }
                }
            }

            Console.WriteLine($"You escaped the maze with {playerHp} HP!");
        }
    }
}