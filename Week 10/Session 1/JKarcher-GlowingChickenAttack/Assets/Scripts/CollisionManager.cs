using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    /* Method: OnCollisionEnter
     * Purpose: check collisions between the animals and anything else they are meant to collide with
     * Restrictions: None
     */
    private void OnCollisionEnter(Collision other)
    {
        // all animals and food 
        // Note: be sure to check a food collision before a player collision
        // if the collision was between food and the animal
        if((gameObject.tag == "Slow") && (other.gameObject.tag == "Food"))
        {
            // add to the players score
            M_Player.player.score += 1;
            // destroy the food so it cant collide with other animals
            Destroy(other.gameObject);
            // face away from the player
            gameObject.transform.LookAt(new Vector3(M_Player.player.playerPrefab.transform.position.x, 0, M_Player.player.playerPrefab.transform.position.z) * -1);
            // set the trigger to deactivate the navMesh Agent and activate the transform Translate
            gameObject.GetComponent<AnimalMoveControl>().hitByFood = true;
        }
        if ((gameObject.tag == "Medium") && (other.gameObject.tag == "Food"))
        {
            M_Player.player.score += 2;
            Destroy(other.gameObject);
            gameObject.transform.LookAt(new Vector3(M_Player.player.playerPrefab.transform.position.x, 0, M_Player.player.playerPrefab.transform.position.z) * -1);
            gameObject.GetComponent<AnimalMoveControl>().hitByFood = true;
        }
        if ((gameObject.tag == "Fast") && (other.gameObject.tag == "Food"))
        {
            M_Player.player.score += 2;
            Destroy(other.gameObject);
            gameObject.transform.LookAt(new Vector3(M_Player.player.playerPrefab.transform.position.x, 0, M_Player.player.playerPrefab.transform.position.z) * -1);
            gameObject.GetComponent<AnimalMoveControl>().hitByFood = true;
        }
        // all animals and player
        // if the collision was between animal and the player
        if ((gameObject.tag == "Slow") && (other.gameObject.tag == "Player"))
        {
            // activate the player death condidion
            // all active animals will still walk off the map
            // deactivate further spawning
            M_Player.player.Death();
        }
        if ((gameObject.tag == "Medium") && (other.gameObject.tag == "Player"))
        {
            M_Player.player.Death();
        }
        if ((gameObject.tag == "Fast") && (other.gameObject.tag == "Player"))
        {
            M_Player.player.Death();
        }
    }
}
