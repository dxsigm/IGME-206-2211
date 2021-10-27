using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateLevelUp : MonoBehaviour
{
    float timer = 0;
    float timerRise = 0;
    float speed = 0;
    [SerializeField] GameObject Center;
    [SerializeField] GameObject Level2;
    [SerializeField] GameObject Level3;
    [SerializeField] GameObject Level4;
    [SerializeField] GameObject Level5;
    [SerializeField] GameObject player;
    bool rise = false;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerRise += Time.deltaTime;
        // set the delay trigger for the player to begin to rise
        if(timerRise >= 10)
        {
            rise = true;
        }
        // after 20s automatically load the next scene
        if(timerRise >= 20)
        {
            SceneManager.LoadScene(1);
        }
        // skip level up scene
        if (Input.anyKey)
        {
            SceneManager.LoadScene(1);
        }
        // incrament the speed up
        if (timer >= 2)
        {
            speed+=.01f;
        }
        // cap the speed to 2
        if(speed > 2f)
        {
            speed = 2f;
        }
        // rotate the platform in oposing directions
        Center.transform.eulerAngles = new Vector3(Center.transform.eulerAngles.x, Center.transform.eulerAngles.y + speed, Center.transform.eulerAngles.z);
        Level2.transform.eulerAngles = new Vector3(Level2.transform.eulerAngles.x, Level2.transform.eulerAngles.y - speed / 2f, Level2.transform.eulerAngles.z);
        Level3.transform.eulerAngles = new Vector3(Level3.transform.eulerAngles.x, Level3.transform.eulerAngles.y + speed / 2.5f, Level3.transform.eulerAngles.z);
        Level4.transform.eulerAngles = new Vector3(Level4.transform.eulerAngles.x, Level4.transform.eulerAngles.y - speed / 3f, Level4.transform.eulerAngles.z);
        Level5.transform.eulerAngles = new Vector3(Level5.transform.eulerAngles.x, Level5.transform.eulerAngles.y + speed / 3.5f, Level5.transform.eulerAngles.z);
        // delay before the player begins to rise
        if (rise)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + .01f, player.transform.position.z);
        }
    }
}
