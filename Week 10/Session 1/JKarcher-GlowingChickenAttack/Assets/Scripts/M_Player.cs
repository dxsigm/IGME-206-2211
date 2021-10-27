using PlayerObject;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_Player : MonoBehaviour
{
    // Gives me one player object to reference
    public static Player player = new Player();
    bool informationTrigger = false;
    [SerializeField] GameObject information;
    [SerializeField] GameObject title;

    public void Update()
    {
        // display the information screen
        information.SetActive(informationTrigger);
        // display the title
        title.SetActive(!informationTrigger);
    }
    /* Method: StartGame
     * Purpose: To load everything nessisary to enter the main game scene
     * Restrictions: None
     */
    public void StartGame()
    {
        // load the main game scene
        SceneManager.LoadScene(1);
    }
    /* Method: Exit
     * Purpose: To exit the game
     * Restrictions: None
     */
    public void Exit()
    {
        // exit the game
        Application.Quit();
    }
    /* Method: Information
     * Purpose: to change whether or not the information is being displayed on the screen
     * Restrictions: None
     */
    public void Information()
    {
         // show or hide the information screen
        informationTrigger = !informationTrigger;
    }
}
