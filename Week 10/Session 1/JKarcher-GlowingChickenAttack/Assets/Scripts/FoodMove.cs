using UnityEngine;

public class FoodMove : MonoBehaviour
{
    float speed = 40;
    [SerializeField] Rigidbody rig;
    Vector3 direction = new Vector3(0,1,0);

    // Update is called once per frame
    void Update()
    {
        // if there is a RigidBody
        if(rig != null)
        {
            // Move the object forward
            rig.MovePosition(transform.position + (Vector3.forward * speed) * Time.deltaTime);
            // rotate the food model
            gameObject.transform.GetChild(0).gameObject.transform.Rotate(direction * 5);
        }
    }
}
