using UnityEngine;
using UnityEngine.AI;

public class AnimalMoveControl : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    GameObject ground;
    public bool hitByFood;
    float mapBorderZMin;
    float mapBorderZMax;
    float mapBorderXMin;
    float mapBorderXMax;
    void Start()
    {
        // assign the NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        // assign the speed of the navMeshAgent based on how fast the animal is meant to move
        if (gameObject.tag == "Slow")
        {
            agent.speed = 30;
        }
        if (gameObject.tag == "Meduim")
        {
            agent.speed = 35;
        }
        if (gameObject.tag == "Fast")
        {
            agent.speed = 40;
        }
        // find the ground
        ground = GameObject.Find("Environment").transform.GetChild(0).gameObject;
        // find the edges of the map in xz
        mapBorderZMin = ground.transform.position.z - ground.GetComponent<Renderer>().bounds.size.z / 2;
        mapBorderZMax = ground.transform.position.z + ground.GetComponent<Renderer>().bounds.size.z / 2;
        mapBorderXMin = ground.transform.position.x - ground.GetComponent<Renderer>().bounds.size.x / 2;
        mapBorderXMax = ground.transform.position.x + ground.GetComponent<Renderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // if the animal hasnt been hit yet
        if(!hitByFood)
        {
            // run tword the player
            agent.destination = new Vector3(M_Player.player.playerPrefab.transform.position.x, .25f, M_Player.player.playerPrefab.transform.position.z - 5);
        }
        // if the animal has been hit
        else
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().enabled = false;
            // run away from the player
            transform.Translate(Vector3.forward * 40 * Time.deltaTime);
            // nav mesh agent is still active but not influencing movement
            agent.destination = transform.position;
        }
        // if the animal exits the map across any border then destroy it
        if(transform.position.z <= mapBorderZMin)
        {
            Destroy(gameObject);
        }
        else if(transform.position.z >= mapBorderZMax)
        {
            Destroy(gameObject);
        }
        if (transform.position.x <= mapBorderXMin)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x >= mapBorderXMax)
        {
            Destroy(gameObject);
        }
    }
}
