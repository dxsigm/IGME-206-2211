using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabAnimals;
    public PlayerController playerController;

    private ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        float delay = 1.5f - (0.2f * scoreKeeper.level);
        
        if (delay <= 0)
        {
            delay = 0.1f;
        }

        InvokeRepeating("SpawnRandomAnimal", 2.0f, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, prefabAnimals.Length);

        Instantiate(prefabAnimals[animalIndex],
            new Vector3(Random.Range(-playerController.xRange, playerController.xRange), 0, 25), 
            prefabAnimals[animalIndex].transform.rotation);
    }
}
