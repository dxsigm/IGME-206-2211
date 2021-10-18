using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDie : MonoBehaviour
{
    public Rigidbody dieRigidBody;

    public int nDieRollValue = -1;

    // save the values of the die in all 6 directions in a Dictionary indexed on Vector3
    private Dictionary<Vector3, int> dieMap = new Dictionary<Vector3, int>();

    private float prevDieXVelocity;
    private float prevDieYVelocity;
    private float prevDieZVelocity;
    private float prevTime;

    // Start is called before the first frame update
    void Start()
    {
        dieRigidBody = GetComponent<Rigidbody>();

        dieMap[Vector3.up] = 2;
        dieMap[Vector3.down] = 5;
        dieMap[Vector3.left] = 1;
        dieMap[Vector3.right] = 6;
        dieMap[Vector3.forward] = 3;
        dieMap[Vector3.back] = 4;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // while a die value is not set yet and 0.5 seconds has passed
        if (nDieRollValue == -1 && Time.time - prevTime > 0.5f)
        {
            //nDieRollValue = Random.Range(1, 7);

            // if the die is nearly still
            if (Mathf.Abs(prevDieXVelocity) < 0.02f && Mathf.Abs(dieRigidBody.velocity.x) < 0.02f &&
                Mathf.Abs(prevDieYVelocity) < 0.02f && Mathf.Abs(dieRigidBody.velocity.y) < 0.02f &&
                Mathf.Abs(prevDieZVelocity) < 0.02f && Mathf.Abs(dieRigidBody.velocity.z) < 0.02f)
            {
                // calculate the relative up axis of the die to fetch the number facing up
                Vector3 referenceObjectSpace = transform.InverseTransformDirection(Vector3.up);
            
                // find smallest difference to object space direction
                float min = float.MaxValue;
                Vector3 minKey = Vector3.up;
                foreach (Vector3 key in dieMap.Keys )
                {
                    float a = Vector3.Angle(referenceObjectSpace, key);
                    if (a <= 5.0f && a < min)
                    {
                        min = a;
                        minKey = key;
                    }
                }
            
                nDieRollValue = dieMap[minKey];
            }
            
            // save current velocities and time
            prevDieXVelocity = dieRigidBody.velocity.x;
            prevDieYVelocity = dieRigidBody.velocity.y;
            prevDieZVelocity = dieRigidBody.velocity.z;
            
            prevTime = Time.time;
        }
    }

    private void OnMouseDown()
    {
        // set the flag that the die is rolling
        nDieRollValue = -1;
        prevTime = Time.time;

        dieRigidBody.AddForce(Vector3.up * Random.Range(20, 30), ForceMode.Impulse);
        dieRigidBody.AddTorque(Random.Range(-20f, 10f) + 60, Random.Range(-20f, 10f) + 60, Random.Range(-20f, 10f) + 60, ForceMode.Impulse);
    }
}
