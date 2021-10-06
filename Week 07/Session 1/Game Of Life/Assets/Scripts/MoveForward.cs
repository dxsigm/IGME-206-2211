using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "Food")
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.up * Time.deltaTime * 500);
        }
        else if (this.tag == "Animal")
        {
            Vector3 playerVector = GameObject.FindGameObjectWithTag("Player").transform.position - (Vector3.forward * 5);
            Vector3 targetVector = new Vector3(playerVector.x, 0, playerVector.z);
            Vector3 direction = (transform.position - targetVector).normalized;
            transform.Translate(direction * Time.deltaTime * speed, Space.Self);
        }
    }
}
