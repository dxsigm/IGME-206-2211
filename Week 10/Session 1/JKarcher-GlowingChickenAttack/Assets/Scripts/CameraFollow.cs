using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // have the camera follow the player by following the players x position
        transform.position = new Vector3(M_Player.player.playerPrefab.transform.position.x, transform.position.y, transform.position.z);
    }
}
