using UnityEngine;

public class ChangeBackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] backgroundMusic;
    [SerializeField] AudioSource audioSource;
    int WhatClip;
    float timer;
    bool changeOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        // set the misic to a random background music
        WhatClip = Random.Range(0, backgroundMusic.Length - 1);
        audioSource.clip = backgroundMusic[WhatClip];
        // play the background music
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // add to a timer
        timer += Time.deltaTime;
        // if the game isnt over
        if (!M_Player.player.gameOver)
        {
            // check to see if the song is over yet
            if (timer >= backgroundMusic[WhatClip].length)
            {
                // select a new song
                WhatClip = Random.Range(0, backgroundMusic.Length - 1);
                // reset the timer
                timer = 0;
                // assign the new song
                audioSource.clip = backgroundMusic[WhatClip];
                // play the new song
                audioSource.Play();
            }
        }
        // if the game is over
        else
        {
            // and we havent already set the death song to play
            if (changeOnce)
            {
                // stop the current song
                audioSource.Stop();
                // set the death song
                WhatClip = backgroundMusic.Length - 1;
                // assign the death song
                audioSource.clip = backgroundMusic[WhatClip];
                // loop the death song
                audioSource.loop = true;
                // play the death song
                audioSource.Play();
                // set the trigger so we dont try to set the death song again
                changeOnce = false;
            }
        }
    }
}
