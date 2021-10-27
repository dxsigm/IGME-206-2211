using UnityEngine;

public class RotateSky : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // rotate a sphere with a yz offset
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 5 * Time.deltaTime, transform.rotation.eulerAngles.z + 10 * Time.deltaTime);
    }
}
