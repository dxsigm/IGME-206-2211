using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public int score;
    public int nFood;
    public int level;
     
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        score = 0;
        nFood = 10;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        nFood = 10;
        ++level;
        SceneManager.LoadScene("LevelUp");
    }
}
