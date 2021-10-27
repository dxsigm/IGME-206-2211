using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI level;
    void Start()
    {
        // display the player score and level in text
        score.text = "Score: " + M_Player.player.score;
        level.text = "Level: " + M_Player.player.level;
    }

    /* Method: PlayAgain
     * Purpose: to load the main game scene and allow the game to continue
     * Restrictions: None
     */
    public void PlayAgain()
    {
        // set the game over trigger back to false
        M_Player.player.gameOver = false;
        // load the main game scene
        SceneManager.LoadScene(1);
    }
    /* Method: Exit
     * Purpose: exit the game
     * Restrictions: None
     */
    public void Exit()
    {
        Application.Quit();
    }
}
