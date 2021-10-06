using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private PlayerController playerController;
    private ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag == "Food" && other.tag == "Animal")
        {
            if (other.gameObject.name.Contains("Doe"))
            {
                scoreKeeper.score += 1;
            }
            if (other.gameObject.name.Contains("Fox"))
            {
                scoreKeeper.score += 2;
            }
            if (other.gameObject.name.Contains("Moose"))
            {
                scoreKeeper.score += 3;
            }
            Destroy(gameObject);

            other.gameObject.transform.Rotate(new Vector3(0, Random.Range(135, 225)));
            BoxCollider boxCollider = (BoxCollider)other.gameObject.GetComponent<BoxCollider>();
            boxCollider.enabled = false;

            Debug.Log("Pizzas: " + scoreKeeper.nFood + " | Level: " + (scoreKeeper.level + 1) + " | Score: " + scoreKeeper.score);

            // if all food is gone (if scoreKeeper.nFood == 0) we want to level 
            if (scoreKeeper.nFood == 0)
            {
                scoreKeeper.LevelUp();
            }
        }

        if (this.tag == "Player")
        {
            if (other.tag == "Animal" && !playerController.gameOver)
            {
                playerController.Die();
            }
        }
    }
}
