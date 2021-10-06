using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public Button button1;
    public Button button2;
    private ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        button1.onClick.AddListener(TaskOnClickButton1);
        button2.onClick.AddListener(TaskOnClickButton2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClickButton1()
    {
        Application.Quit();
    }
    void TaskOnClickButton2()
    {
        scoreKeeper.level = 0;
        scoreKeeper.nFood = 10;
        scoreKeeper.score = 0;

        SceneManager.LoadScene("Stampede");
    }
}
