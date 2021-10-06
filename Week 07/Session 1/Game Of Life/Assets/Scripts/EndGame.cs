using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Game Over.\nYou reached level " + (scoreKeeper.level + 1) + "\nYour score was " + (scoreKeeper.score);
    }
}
