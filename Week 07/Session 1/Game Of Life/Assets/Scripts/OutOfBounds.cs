using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 30f)
        {
            Destroy(this.gameObject);

            if (this.tag == "Food")
            {
                if (scoreKeeper.nFood == 0)
                {
                    scoreKeeper.LevelUp();
                }
            }
        }

        if (transform.position.z < -15f)
        {
            Destroy(this.gameObject);
        }
    }
}
