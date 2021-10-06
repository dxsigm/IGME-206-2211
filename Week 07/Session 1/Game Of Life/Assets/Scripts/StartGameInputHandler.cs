using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameInputHandler : MonoBehaviour
{
    public Button button1;
    public Button button2;

    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("Stampede");
    }
}
