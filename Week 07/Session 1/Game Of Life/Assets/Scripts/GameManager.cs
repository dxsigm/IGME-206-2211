using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameOfLife;

public class GameManager : MonoBehaviour
{
    public GameObject cellPreFab;
    public Camera mainCamera;
    public int probability;
    public int MAX_ROWS = 40;
    public int MAX_COLS = 80;

    private float prevTime;

    // Start is called before the first frame update
    void Start()
    {
        Game.bExit = true;
        Game.MAX_ROWS = MAX_ROWS;
        Game.MAX_COLS = MAX_COLS;

        Game.CreateOrganism(probability);
        mainCamera.transform.position = new Vector3(Game.MAX_COLS / 4, 30, Game.MAX_ROWS / 2);
        mainCamera.orthographicSize = Game.MAX_ROWS / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int row = 0; row < Game.MAX_ROWS; ++row)
        {
            for (int col = 0; col < Game.MAX_COLS; ++col)
            {
                Game.Cell thisCell = Game.organism[row, col];
                if (thisCell.prevCellState.eAliveState != thisCell.currentCellState.eAliveState)
                {
                    if (thisCell.gameObject != null)
                    {
                        Destroy((GameObject)thisCell.gameObject);
                        thisCell.gameObject = null;
                    }

                    if (thisCell.currentCellState.eAliveState == EAliveState.alive)
                    {
                        Vector3 cellPos = new Vector3(col, 0, row);
                        thisCell.gameObject = Instantiate(cellPreFab, cellPos, cellPreFab.transform.rotation);
                    }

                    thisCell.prevCellState.eAliveState = thisCell.currentCellState.eAliveState;
                }
            }
        }

        if (Time.time - prevTime > 0.01)
        {
            prevTime = Time.time;
            Game.CalculateNextGeneration(Game.organism[0, 0]);
        }
    }
}
