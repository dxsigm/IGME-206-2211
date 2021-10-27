using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] Animals;
    GameObject ground;
    float groundXMax;
    float groundXMin;
    float timerDelay = 2f;
    bool delay = true;
    float timerRate = 1.5f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        // find the ground
        ground = GameObject.Find("Environment").transform.GetChild(0).gameObject;
        // find the maximum spawnable range that is still on the ground
        groundXMin = ground.transform.position.x - ground.GetComponent<Renderer>().bounds.size.x / 2;
        groundXMax = ground.transform.position.x + ground.GetComponent<Renderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // add delta time to our timer
        timer += Time.deltaTime;
        // edit the rate of spawn based on the players level
        timerRate = 1.5f - (M_Player.player.level * .2f);
        // the minimum delay for spawning animals is .1 seconds
        if(timerRate <= .1f)
        {
            timerRate = .1f;
        }
        // initial delay before spawning
        if(delay)
        {
            // once we reached the delay time max then we allow spawning
            if(timer >= timerDelay)
            {
                delay = false;
                timer = 0;
            }
        }
        else
        {
            // spawn at timer rate in seconds
            if(timer >= timerRate)
            {
                SpawnAnimals();
                timer = 0;
            }
        }
        // deactivate spawning if the player dies
        if(M_Player.player.gameOver)
        {
            gameObject.GetComponent<SpawnManager>().enabled = false;
        }
    }
    /* Method: SpawnAnimals
     * Purpose: spawn an animal at a random x point on the far side of the ground
     * Restrictions: None
     */
    void SpawnAnimals()
    {
        GameObject g = Instantiate(Animals[(int)Random.Range(0, Animals.Length)], new Vector3(Random.Range(groundXMin,groundXMax),transform.position.y,transform.position.z), Quaternion.Euler(new Vector3(0,180,0)));
    }
}
