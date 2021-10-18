using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LinkedListNode<Cell> currentCell;
    public LinkedListNode<Cell> targetCell;
    public string sDirection = "+";
    public int nSpacesLeft;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // because GameManager.Update() checks for currentPlayerClassObject.nSpacesLeft == 0 for the player finishing their turn
        // we need a local variable to save it and recalculate it because as soon as we set it to 0, the player's turn ends.
        // this is not a good thing if the player lands on a power-up, penalty, chute or ladder
        int localNSpacesLeft = nSpacesLeft;

        // if more spaces left to move
        if (localNSpacesLeft > 0)
        {
            // indicate whether the next cell to move to is defined
            bool bNullTargetCell = (targetCell == null);

            // if we have a cell to move to and the player is more than 0.1 distance from it
            if (!bNullTargetCell && Vector3.Distance(transform.position, targetCell.Value.position) >= 0.1f)
            {
                // calculate the rotation of the player
                if (currentCell.Value.nNumber == 1 || (currentCell.Value.nNumber - 1) % 7 > 0)
                {
                    if ((currentCell.Value.nNumber - 1) / 7 % 2 == 0)
                    {
                        if( sDirection == "+")
                        {
                            transform.eulerAngles = new Vector3(0, 90, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, -90, 0);
                        }
                    }
                    else
                    {
                        if (sDirection == "+")
                        {
                            transform.eulerAngles = new Vector3(0, -90, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }
                }

                if (currentCell.Value.nNumber > 0 && currentCell.Value.nNumber % 7 == 0)
                {
                    if( sDirection == "+")
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0,180, 0);
                    }
                }

                // gradually walk the player to the next cell
                transform.Translate((targetCell.Value.position - transform.position).normalized * 7 * Time.deltaTime, Space.World);
            }

            // if no target cell set yet, set it to the current cell
            if (bNullTargetCell)
            {
                targetCell = currentCell;
            }

            // if the player is within 0.1 of the target cell
            if (Vector3.Distance(transform.position, targetCell.Value.position) < 0.1f )
            {
                // set the current cell to the destination cell
                currentCell = targetCell;

                // if we were moving toward a valid cell
                if (!bNullTargetCell)
                {
                    // decrement the number of spaces left to move
                    --localNSpacesLeft;
                }

                // move through the linked list based on the direction of travel
                if (sDirection == "+")
                {
                    if (targetCell.Next != null)
                    {
                        targetCell = targetCell.Next;
                    }
                }
                else
                {
                    if (targetCell.Previous != null)
                    {
                        targetCell = targetCell.Previous;
                    }
                }
            }
        }

        // if there are no spaces left to move, we are on the final cell of our turn (perhaps)
        if (localNSpacesLeft == 0)
        {
            // if the cell has a penalty
            if (currentCell.Value.hasPenalty)
            {
                // clear the penalty flag
                currentCell.Value.hasPenalty = false;

                // destroy the penalty object
                Destroy(currentCell.Value.penaltyGameObject);

                // reverse direction
                sDirection = "-";

                // set the moves left to 2 
                localNSpacesLeft = 2;

                // set the target cell to the previous cell
                targetCell = currentCell.Previous;
            }
            else if (currentCell.Value.hasPowerUp)
            {
                // clear the powerup flag
                currentCell.Value.hasPowerUp = false;

                // destroy the penalty object
                Destroy(currentCell.Value.powerUpGameObject);

                // ensure we are going forward
                sDirection = "+";

                // set the moves left to 2 
                localNSpacesLeft = 2;

                // set the target cell to the next cell
                targetCell = currentCell.Next;

                // increment the player's score
                if( Statics.nPlayer == 1)
                {
                    ++Statics.playerOneScore;
                }
                else
                {
                    ++Statics.playerTwoScore;
                }
            }
            else if (currentCell.Value.shortcut != null)
            {
                // otherwise a shortcut (chute or ladder)

                // rotate toward the board
                transform.eulerAngles = new Vector3(0, 0, 0);

                // set target cell to the other side of the shortcut
                targetCell = currentCell.Value.shortcut;

                // move one space
                localNSpacesLeft = 1;
            }
        }

        // set the class-scoped variable to the local variable
        nSpacesLeft = localNSpacesLeft;
    }

    private void OnMouseDown()
    {
        // rotate 180 degrees if the player is clicked only if not moving
        if (nSpacesLeft == -1)
        {
            transform.Rotate(Vector3.up, 180);
            sDirection = (sDirection == "+" ? "-" : "+");
        }
    }
}

