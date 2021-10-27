using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerObject
{
    public class Player
    {
        public Animator anim;
        public Rigidbody rig;
        public Vector3 direction;
        public GameObject foodPrefab;
        public GameObject playerPrefab;
        public ParticleSystem smoke;
        public AudioSource audioS;
        public AudioClip[] jumpSound;
        public AudioClip[] deathSound;

        public float speed;
        private float jumpHeight;

        public bool animRunning;
        public bool canJump;
        public bool gameOver;

        public int foodCount;
        public int score;
        public int level;

        #region Constructor
        /* Method: Player
         * Purpose: construct a player
         * Restrictions: None
         */
        public Player()
        {
            anim = null;
            rig = null;
            jumpSound = null;
            direction = Vector3.zero;
            speed = 10;
            jumpHeight = 500f;
            animRunning = false;
            canJump = true;
            foodCount = 10;
            score = 0;
            level = 0;
        }
        #endregion
        #region Movement Control
        /* Method: Move
         * Purpose: Calculate the player movement direction with the player speed and move with the attached Rigidbody
         * Restrictions: None
         */
        public void Move(Transform tPlayer)
        {
            // make sure we have a Rigidbody
            if (rig != null)
            {
                // is the player still alive
                if (!gameOver)
                {
                    rig.MovePosition(tPlayer.position + (direction * speed) * Time.deltaTime);
                }
            }
        }
        #endregion
        #region Animation Control
        /* Method: AnimationReset
         * Purpose: Reset all animation values to idle state
         * Restrictions: canJump can not be reset here or it allows double jumping
         */
        public void AnimationReset()
        {
            // verify that we have an animator
            if (anim != null)
            {
                animRunning = false;
                anim.SetFloat("Speed_f", 0f);
                anim.SetBool("Jump_b", false);
                anim.SetBool("Falling_b", false);
            }

        }
        /* Method: Running
         * Purpose: Perform all internal actions required to set the animation to run
         * Restrictions: None
         */
        public void Running()
        {
            if (anim != null)
            {
                // since running is a loop animation make sure the animation isnt already playing
                if (!animRunning)
                {
                    // make sure all other animations have stopped
                    AnimationReset();
                    // set the running trigger to true
                    animRunning = true;
                    // set the animation to running
                    anim.SetFloat("Speed_f", 5f);
                }
            }
        }
        /* Method: Idle
         * Purpose: Perform all internal actions required to set the animation to Idle
         * Restrictions: None
         */
        public void Idle()
        {
            if (anim != null)
            {
                AnimationReset();
                // set the animation to idle
                anim.SetFloat("Speed_f", 0f);
            }
        }
        /* Method: Jump
         * Purpose: Perform all internal actions required for the player to jump
         * Restrictions: none
         */
        public void Jump()
        {
            if (anim != null)
            {
                if (!gameOver)
                {
                    // set the trigger to false
                    canJump = false;
                    // make sure we have an audio source
                    if (audioS != null)
                    {
                        // play the jump sound
                        // i still like jump 2 most but all are posible now
                        audioS.clip = jumpSound[1];
                        audioS.Play();
                    }
                    // add an upward force to the player
                    rig.AddForce(Vector3.up * jumpHeight);
                    // set the animation to jump
                    anim.SetBool("Jump_b", true);
                }
            }
        }
        /* Method: Falling
         * Purpose: To prevent an animation change if the player changes direction in the air
         * Restrictions: None
         */
        public void Falling()
        {
            if (anim != null)
            {
                if (!canJump)
                {
                    // set the animation to falling
                    anim.SetBool("Falling_b", true);
                }
            }
        }
        /* Method: LandingGround
         * Purpose: Perform all internal actions required when the player lands on the ground
         * Restrictions: None
         */
        public void LandingGround()
        {
            if (anim != null)
            {
                if (canJump)
                {
                    // stop the animation early if the chatacter touches the ground
                    anim.SetBool("Jump_b", false);
                    anim.SetBool("Falling_b", false);
                }
            }
        }
        /* Method: ThrowFood
         * Purpose: Perform all internal actions required when the player throws food
         * Restrictions: None
         */
        public void ThrowFood()
        {
            if (anim != null)
            {
                // if the player is not hit
                if (!gameOver)
                {
                    // if we cant find a food item
                    if (!GameObject.Find("Food"))
                    {
                        // player is still has food to throw
                        if (foodCount > 0)
                        {
                            // if the player is on the ground
                            if (canJump)
                            {
                                // make a food object
                                GameObject gFood = GameObject.Instantiate(foodPrefab, new Vector3(playerPrefab.transform.position.x, playerPrefab.transform.position.y, playerPrefab.transform.position.z + 1), Quaternion.identity);
                                // rename the food
                                gFood.name = "Food";
                                // automaticly destroy the food after 1.5 seconds
                                GameObject.Destroy(gFood, 1.5f);
                                // reduce the amount of food the player has
                                foodCount--;
                            }
                        }
                    }
                }
            }
        }
        /* Method: Death
         * Purpose: Perform all internal actions required when the player dies
         * Restrictions: None
         */
        public void Death()
        {
            if (audioS != null)
            {
                // play the jump sound
                audioS.clip = deathSound[Random.Range(0, deathSound.Length)];
                audioS.Play();
            }
            if (anim != null)
            {
                // triggers the end of game condition for the game
                gameOver = true;
                // play the death particle
                smoke.Play();
                // set the animation to death
                anim.SetBool("Death_b", true);
                // randomly select death pose
                anim.SetInteger("DeathType_int", (int)Random.Range(1, 3));
            }
        }
        /* Method: LevelUp
         * Purpose: Perform all internal actions required when the player levels up
         * Restrictions: None
         */
        public void LevelUp()
        {
            // reset the food count
            foodCount = 10;
            // increase the level
            level++;
            // load the level up scene
            SceneManager.LoadScene(3);
        }
        #endregion
    }
}