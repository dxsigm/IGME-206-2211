using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cell
{
    public LinkedListNode<Cell> shortcut;
    public GameObject floorGameObject;
    public GameObject ascendingGameObject;
    public GameObject powerUpGameObject;
    public GameObject penaltyGameObject;
    public Vector3 position;
    public int nNumber;
    public bool hasPowerUp;
    public bool hasPenalty;
}

public class Statics
{
    public static int level = 3;
    public static int nPlayer;
    public static int playerOneScore = 0;
    public static int playerTwoScore = 0;
}


public class GameManager : MonoBehaviour
{
    public GameObject floorObject;
    public GameObject ladderObject;
    public GameObject chuteObject;
    public GameObject finishChute;
    public GameObject stairObject;
    public GameObject penaltyObject;
    public GameObject powerupObject;
    public GameObject dieObject;

    public GameObject playerOneGameObject;
    public GameObject playerTwoGameObject;

    public Camera mainCamera;
    public Camera player1Camera;
    public Camera player2Camera;


    public LinkedList<Cell> board = new LinkedList<Cell>();

    private RollDie rollDieClassObject;
    private PlayerController playerOneClassObject;
    private PlayerController playerTwoClassObject;

    private PlayerController currentPlayer;

    private GameObject currentPlayerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 cameraPositionOffset = new Vector3(0, (Statics.level - 1) * 5, (Statics.level - 1) * -3); 
        mainCamera.transform.position += cameraPositionOffset;

        rollDieClassObject = GameObject.Find("WhiteDie").GetComponent<RollDie>();

        player1Camera.enabled = false;
        player2Camera.enabled = false;

        playerOneClassObject = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerTwoClassObject = GameObject.Find("Player2").GetComponent<PlayerController>();

        bool bGameOver = false;

        int nLadders;
        int nChutes;
        int nPenalties;
        int nPowerUps;

        //while (!bGameOver)
        {
            List<int> lLadders = new List<int>();
            List<int> lChutes = new List<int>();
            List<int> lPowerUps = new List<int>();
            List<int> lPenalties = new List<int>();

            SortedList<int, LinkedListNode<Cell>> slLadders = new SortedList<int, LinkedListNode<Cell>>();
            SortedList<int, LinkedListNode<Cell>> slChutes = new SortedList<int, LinkedListNode<Cell>>();

            Cell cell = null;

            nPenalties = Statics.level + 1;
            nPowerUps = Statics.level + 1;
            nLadders = Statics.level + 1;
            nChutes = Statics.level;

            //   15 16 17 18 19 20 21 22
            //   14 13 12 11 10 9  8
            // 0 1  2  3  4  5  6  7
            //
            // 2 * ((n + 7) / 7) * 7 - (n - 1)

            while (nLadders > 0)
            {
                int thisEl = Random.Range(1, (Statics.level + 1) * 7);
                if (thisEl % 7 == 0 || lLadders.Contains(thisEl))
                {
                    continue;
                }

                lLadders.Add(thisEl);
                --nLadders;
            }

            // add the "upstairs" cell
            int nLength = lLadders.Count;
            for (int i = 0; i < nLength; ++i)
            {
                lLadders.Add(2 * ((lLadders[i] + 7) / 7) * 7 - (lLadders[i] - 1));
            }

            while (nChutes > 0)
            {
                int thisEl = Random.Range(1, (Statics.level + 1) * 7);
                if (thisEl % 7 == 0 || lLadders.Contains(thisEl) || lChutes.Contains(thisEl))
                {
                    continue;
                }

                lChutes.Add(thisEl);
                --nChutes;
            }

            // add the "upstairs" cell
            nLength = lChutes.Count;
            for (int i = 0; i < nLength; ++i)
            {
                lChutes.Add(2 * ((lChutes[i] + 7) / 7) * 7 - (lChutes[i] - 1));
            }

            while (nPowerUps > 0)
            {
                int thisEl = Random.Range(1, (Statics.level + 2) * 7 + 1);
                if (thisEl % 7 == 0 || lPowerUps.Contains(thisEl))
                {
                    continue;
                }

                lPowerUps.Add(thisEl);
                --nPowerUps;
            }

            while (nPenalties > 0)
            {
                int thisEl = Random.Range(1, (Statics.level + 2) * 7 + 1);
                if (thisEl % 7 == 0 || lPenalties.Contains(thisEl) || lPowerUps.Contains(thisEl))
                {
                    continue;
                }

                lPenalties.Add(thisEl);
                --nPenalties;
            }

            float x = -5f;

            // insert starting cell
            cell = new Cell();
            cell.nNumber = 0;
            cell.position = new Vector3(x, 0, 0);
            cell.floorGameObject = Instantiate(floorObject, cell.position, floorObject.transform.rotation);
            board.AddLast(cell);

            int cntr = 0;

            for (cntr = 1; cntr < (Statics.level + 2) * 7 + 1; ++cntr)
            {
                if (cntr == 1 || (cntr - 1) % 7 > 0)
                {
                    if ((cntr - 1) / 7 % 2 == 0)
                    {
                        x += 5.0f;
                    }
                    else
                    {
                        x -= 5.0f;
                    }
                }

                cell = new Cell();
                cell.nNumber = cntr;
                cell.position = new Vector3(x, ((cntr - 1) / 7) * 6, ((cntr - 1) / 7) * 5);
                cell.floorGameObject = Instantiate(floorObject, cell.position, floorObject.transform.rotation);

                if (cntr % 7 == 0 && cntr < (Statics.level + 2) * 7)
                {
                    cell.ascendingGameObject = Instantiate(stairObject, cell.position + new Vector3(0, 0, 4), stairObject.transform.rotation);
                }

                if (lPowerUps.Contains(cntr))
                {
                    cell.hasPowerUp = true;
                    cell.powerUpGameObject = Instantiate(powerupObject, cell.position + new Vector3(0, 3, -1), powerupObject.transform.rotation);
                }

                if (lPenalties.Contains(cntr))
                {
                    cell.hasPenalty = true;
                    cell.penaltyGameObject = Instantiate(penaltyObject, cell.position + new Vector3(0, 3, -1), penaltyObject.transform.rotation);
                }

                if (lLadders.Contains(cntr) &&
                    lLadders.Contains(2 * ((cntr + 7) / 7) * 7 - (cntr - 1)))
                {
                    cell.ascendingGameObject = Instantiate(ladderObject, cell.position + new Vector3(0, 6, 2), ladderObject.transform.rotation);
                }

                if (lChutes.Contains(cntr) &&
                    lChutes.Contains(2 * ((cntr + 7) / 7) * 7 - (cntr - 1)))
                {
                    cell.ascendingGameObject = Instantiate(chuteObject, cell.position + new Vector3(0, 8, 3), chuteObject.transform.rotation);
                }

                board.AddLast(cell);

                if (lLadders.Contains(cntr))
                {
                    slLadders[cntr] = board.Last;
                }

                if (lChutes.Contains(cntr))
                {
                    slChutes[cntr] = board.Last;
                }
            }

            foreach (int thisEl in lLadders)
            {
                int nUpstairs = 2 * ((thisEl + 7) / 7) * 7 - (thisEl - 1);
                if (slLadders.Keys.Contains(nUpstairs))
                {
                    slLadders[thisEl].Value.shortcut = slLadders[nUpstairs];
                }
            }

            foreach (int thisEl in lChutes)
            {
                int nUpstairs = 2 * ((thisEl + 7) / 7) * 7 - (thisEl - 1);
                if (slChutes.Keys.Contains(nUpstairs))
                {
                    slChutes[nUpstairs].Value.shortcut = slChutes[thisEl];
                }
            }

            cell = new Cell();
            cell.nNumber = cntr;
            cell.position = new Vector3(x + (Statics.level % 2 == 1 ? 1 : -1) * 5.0f, ((cntr - 2) / 7) * 6, ((cntr - 2) / 7) * 5);
            cell.floorGameObject = Instantiate(floorObject, cell.position, floorObject.transform.rotation);
            cell.ascendingGameObject = Instantiate(finishChute, cell.position + new Vector3((Statics.level % 2 == 1 ? 1 : -1) * 3, 2, 0), finishChute.transform.rotation);
            if (Statics.level % 2 == 0)
            {
                cell.ascendingGameObject.transform.Rotate(0, 180, 0, Space.World);
            }

            board.AddLast(cell);

            string szBoard = "";
            LinkedListNode<Cell> linkedListNode = board.First;

            while (linkedListNode != null)
            {
                Cell thisCell = linkedListNode.Value;
                Cell thisShortcut = null;
                if (thisCell.shortcut != null)
                {
                    thisShortcut = thisCell.shortcut.Value;
                }

                szBoard += $"[{thisCell.nNumber}" +
                    $"{(playerOneClassObject.targetCell == linkedListNode ? "^P1^" : "")}" +
                    $"{(playerOneClassObject.targetCell == linkedListNode ? "^P2^" : "")}" +
                    $"{(thisCell.hasPowerUp ? "$" : "")}" +
                    $"{(thisCell.hasPenalty ? "!" : "")}" +
                    $"{(thisShortcut != null ? "=>" + thisShortcut.nNumber : "")}]";

                linkedListNode = linkedListNode.Next;
            }

            Debug.Log(szBoard);

            // place the player at the start of the gameboard
            playerOneClassObject.currentCell = board.First;
            playerOneClassObject.targetCell = null;

            // initialize the number of spaces the user has left to move to -1 indicating no more spaces to move
            playerOneClassObject.nSpacesLeft = -1;

            // place the player at the start of the gameboard
            playerTwoClassObject.currentCell = board.First;
            playerTwoClassObject.targetCell = null;

            // initialize the number of spaces the user has left to move to -1 indicating no more spaces to move
            playerTwoClassObject.nSpacesLeft = -1;

            // put the players' game objects at the start as well
            playerOneGameObject.transform.position = board.First.Value.position;
            playerTwoGameObject.transform.position = board.First.Value.position;

            // start with player one
            currentPlayer = playerOneClassObject;
            currentPlayerGameObject = playerOneGameObject;
            Statics.nPlayer = 1;

            ////  The Game logic from the console application
            ////    player1.currentCell = board.First;
            ////    player2.currentCell = board.First;
            ////
            ////    Player thisPlayer = player1;
            ////
            ////    while (player1.currentCell != board.Last &&
            ////           player2.currentCell != board.Last)
            ////    {
            ////        PrintBoard(board, player1, player2);
            ////
            ////        if (thisPlayer == player1)
            ////        {
            ////            Console.Write("Player 1: Which direction to move (-/+): ");
            ////        }
            ////        else
            ////        {
            ////            Console.Write("Player 2: Which direction to move (-/+): ");
            ////        }
            ////
            ////        string sDirection = Console.ReadLine();
            ////
            ////        if (sDirection.Length == 0)
            ////        {
            ////            sDirection = "+";
            ////        }
            ////
            ////        int nRoll = rand.Next(1, 7);
            ////        Console.WriteLine("You rolled a " + nRoll);
            ////
            ////    move:
            ////        while (nRoll > 0 && thisPlayer.currentCell != null)
            ////        {
            ////            if (sDirection == "+")
            ////            {
            ////                if (thisPlayer.currentCell.Next != null)
            ////                {
            ////                    thisPlayer.currentCell = thisPlayer.currentCell.Next;
            ////                }
            ////                else
            ////                {
            ////                    break;
            ////                }
            ////            }
            ////            else
            ////            {
            ////                if (thisPlayer.currentCell.Previous != null)
            ////                {
            ////                    thisPlayer.currentCell = thisPlayer.currentCell.Previous;
            ////                }
            ////                else
            ////                {
            ////                    break;
            ////                }
            ////            }
            ////
            ////            --nRoll;
            ////        }
            ////
            ////        if (thisPlayer.currentCell.Value.hasPenalty)
            ////        {
            ////            thisPlayer.currentCell.Value.hasPenalty = false;
            ////            PrintBoard(board, player1, player2);
            ////            nRoll = 2;
            ////            sDirection = "-";
            ////            goto move;
            ////        }
            ////
            ////        if (thisPlayer.currentCell.Value.hasPowerUp)
            ////        {
            ////            thisPlayer.currentCell.Value.hasPowerUp = false;
            ////            PrintBoard(board, player1, player2);
            ////            nRoll = 2;
            ////            sDirection = "+";
            ////            ++thisPlayer.score;
            ////            goto move;
            ////        }
            ////
            ////        if (thisPlayer.currentCell.Value.shortcut != null)
            ////        {
            ////            PrintBoard(board, player1, player2);
            ////            thisPlayer.currentCell = thisPlayer.currentCell.Value.shortcut;
            ////            nRoll = 0;
            ////            goto move;
            ////        }
            ////
            ////        if (thisPlayer == player1)
            ////        {
            ////            thisPlayer = player2;
            ////        }
            ////        else
            ////        {
            ////            thisPlayer = player1;
            ////        }
            ////    }
            ////
            ////    if (player1.currentCell == board.Last)
            ////    {
            ////        Console.WriteLine("Player 1 you Won!!!");
            ////    }
            ////    else
            ////    {
            ////        Console.WriteLine("Player 2 you Won!!!");
            ////    }
            ////
            ////    ++Statics.level;
            ////    Console.WriteLine("Moving up to Statics.level " + Statics.level);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if we received a value from the RollDie class
        if (rollDieClassObject.nDieRollValue > 0)
        {
            // disable the main camera
            mainCamera.enabled = false;

            // turn on the player's camera to follow them
            if(currentPlayer == playerOneClassObject)
            {
                player1Camera.enabled = true;
            }
            else
            {
                player2Camera.enabled = true;
            }

            // initialize the number of spaces for the current player to move to the die value
            currentPlayer.nSpacesLeft = rollDieClassObject.nDieRollValue;

            // disable the die from returning a new value
            rollDieClassObject.nDieRollValue = -2;
        }

        // if the player is done moving
        if (currentPlayer.nSpacesLeft == 0)
        {
            // rotate the player to the correct direction based on the level of the board and the current sDirection
            if ((currentPlayer.currentCell.Value.nNumber - 1) / 7 % 2 == 0)
            {
                if (currentPlayer.sDirection == "+")
                {
                    currentPlayerGameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else
                {
                    currentPlayerGameObject.transform.eulerAngles = new Vector3(0, -90, 0);
                }
            }
            else
            {
                if (currentPlayer.sDirection == "+")
                {
                    currentPlayerGameObject.transform.eulerAngles = new Vector3(0, -90, 0);
                }
                else
                {
                    currentPlayerGameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                }
            }

            // this player is done moving
            currentPlayer.nSpacesLeft = -1;

            // switch the current player variables to the next player
            currentPlayer = (currentPlayer == playerOneClassObject ? playerTwoClassObject : playerOneClassObject);
            currentPlayerGameObject = (currentPlayerGameObject == playerOneGameObject ? playerTwoGameObject : playerOneGameObject);
            Statics.nPlayer = (Statics.nPlayer == 1) ? 2 : 1;

            // initialize the current player's next cell to move to null
            currentPlayer.targetCell = null;

            // disable the players' cameras
            player1Camera.enabled = false;
            player2Camera.enabled = false;

            // enable the main camera
            mainCamera.enabled = true;
        }

        // if one of the players won
        if (playerOneClassObject.currentCell == board.Last ||
            playerTwoClassObject.currentCell == board.Last)
        {
            if (playerOneClassObject.currentCell == board.Last)
            {
                Debug.Log("Player One Won!  Level up!");
                ++Statics.playerOneScore;
            }
            else
            {
                Debug.Log("Player Two Won!  Level up!");
                ++Statics.playerTwoScore;
            }

            // level up!
            ++Statics.level;

            // reload the scene with the new level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

