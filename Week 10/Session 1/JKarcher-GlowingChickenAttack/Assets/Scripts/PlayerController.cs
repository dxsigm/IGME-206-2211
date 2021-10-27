using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    RaycastHit hitWhat;
    GameObject ground;
    [SerializeField] ParticleSystem dirtSpray;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] AudioClip[] jumpSound;
    [SerializeField] AudioClip[] deathSound;
    [SerializeField] GameObject foodPrefab;
    [SerializeField] Rigidbody rig;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource audioS;
    float mapBorder;
    float jumpTimer = 1f;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        // find the ground
        ground = GameObject.Find("Environment").transform.GetChild(0).gameObject;
        // connect the player data to the player object
        M_Player.player.playerPrefab = gameObject;
        M_Player.player.rig = rig;
        M_Player.player.anim = anim;
        M_Player.player.audioS = audioS;
        M_Player.player.jumpSound = jumpSound;
        M_Player.player.deathSound = deathSound;
        M_Player.player.foodPrefab = foodPrefab;
        M_Player.player.smoke = smoke;
        // base the distance left or right on the size of the ground as it can be seen
        // orient the moveable area by the center
        // subtract an additional buffer just so our player dosnt fall off the edge
        mapBorder = ground.GetComponent<Renderer>().bounds.size.x / 2 - 1;
    }

    // Update is called once per frame
    void Update()
    {

        #region Movement and Border Control
        // prevent the player from moving in another direction in the air
        // Note: the point that the mouse is over is the point that the player jumps to never past
        if (M_Player.player.canJump)
        {
            // get the position of the mouse on the screen
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            // map the position of the mouse to the player transform and replace the x position
            target = new Vector3(target.x, transform.position.y, transform.position.z);
            // get keybord input from the player
            M_Player.player.direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            // get mouse directional input from the player
            M_Player.player.direction = (target - transform.position).normalized;
        }
        // if the player is within the boundaries then they can move
        if (transform.position.x < mapBorder && transform.position.x > mapBorder * -1)
        {
            //move the player if the distance between the point found and the player is more than 5
            if (Mathf.Abs(target.x - transform.position.x) > 5)
            {
                M_Player.player.Move(transform);
                // allow the player to finish their jump, see note
                if (!M_Player.player.canJump)
                {
                    if(M_Player.player.direction.x > 0)
                    {
                        target = new Vector3(Mathf.Infinity, transform.position.y, transform.position.z);
                    }
                    if (M_Player.player.direction.x < 0)
                    {
                        target = new Vector3(Mathf.Infinity * -1, transform.position.y, transform.position.z);
                    }
                }
            }
            else
            {
                // reset the animation to idle when the player isnt moving
                // Note: this overrides keybord input
                M_Player.player.direction = Vector3.zero;
            }
        }
        // if the player is not within the boundries then put them back inside
        else if (transform.position.x > mapBorder)
        {
            transform.position = new Vector3(mapBorder - .01f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < mapBorder * -1)
        {
            transform.position = new Vector3(mapBorder * -1 + .01f, transform.position.y, transform.position.z);
        }
        #endregion
        #region Orientation and Animation
        // if the player is still alive then they can move and reorient themselves unless they are already in the air
        if (!M_Player.player.gameOver && M_Player.player.canJump)
        {
            // if the player is moving right
            if (M_Player.player.direction.x > 0)
            {
                // face right
                transform.eulerAngles = new Vector3(0, 90, 0);
                // activate the running animation if on the ground
                if (M_Player.player.canJump)
                {
                    M_Player.player.Running();
                }
                // otherwise the player is still in the air
                else
                {
                    M_Player.player.Falling();
                }
            }
            // if the player is moving left
            else if (M_Player.player.direction.x < 0)
            {
                // face left
                transform.eulerAngles = new Vector3(0, -90, 0);
                // activate the running animation if on the ground
                if (M_Player.player.canJump)
                {
                    M_Player.player.Running();
                }
                else
                {
                    M_Player.player.Falling();
                }
            }
            // otherwise the player is not moving
            else
            {
                // face forward
                transform.eulerAngles = Vector3.zero;
                if (M_Player.player.canJump)
                {
                    // activate Idle animation
                    M_Player.player.Idle();
                }
                else
                {
                    M_Player.player.Falling();
                }
            }
        }
        #endregion
        #region Particle Effects
        // if the player has not been run over
        if (!M_Player.player.gameOver)
        {
            //Note: Dirtspray is a child of player
            // if the player running trigger is active
            if (M_Player.player.animRunning)
            {
                // if the dirt spray is not already playing
                if (!dirtSpray.isPlaying)
                {
                    // if the player is on the ground
                    if (M_Player.player.canJump)
                    {
                        dirtSpray.Play();
                    }
                    else
                    {
                        dirtSpray.Stop();
                    }
                }
                //if you are in the air
                else if (!M_Player.player.canJump)
                {
                    dirtSpray.Stop();
                }
            }
            // if you are not moving
            else
            {
                dirtSpray.Stop();
            }
        }
        // if the player has been run over
        else
        {
            dirtSpray.Stop();
        }
        #endregion
        #region Jumping control
        // delay timer to prevent the player from being able to double jump
        jumpTimer += Time.deltaTime;
        // if the player is alowed to jump
        if(M_Player.player.canJump)
        {
            // ensure the delay is long enough to know that the players raycast cant detect the ground anymore
            if(jumpTimer >= 1)
            {
                // if the player pressed the jump axis button
                // axis bindings are set to up, W, and space
                if (Input.GetButtonDown("Jump"))
                {
                    // perform all internal actions requiered for the player to jump
                    M_Player.player.Jump();
                    // reset the player jump timer
                    jumpTimer = 0f;
                }
            }
        }
        // cast a short ray at the players position downward
        if(Physics.Raycast(transform.position, Vector3.down, out hitWhat, .01f))
        {
            // if we hit somthing then check if its the ground
            if(hitWhat.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // animation caontrols for landing on the ground
                M_Player.player.LandingGround();
                // set the player jump trigger to true
                M_Player.player.canJump = true;
            }
            // if the thing hit wasnt the ground then the player cant jump
            else
            {
                M_Player.player.canJump = false;
            }
        }
        // if the player didnt hit anything then the player cant jump
        else
        {
            M_Player.player.canJump = false;
        }
        #endregion
        // if the player presses left mouse button
        if(Input.GetButtonDown("Fire1"))
        {
            // throw a food object
            M_Player.player.ThrowFood();
        }
        // if the player has thrown all food and all food has been despawned
        if((!GameObject.Find("Food"))&&(M_Player.player.foodCount == 0))
        {
            // level up the player
            M_Player.player.LevelUp();
        }
        // display the player score, level, and pizza count in the message line
        Debug.Log(M_Player.player.score +" : "+M_Player.player.level+" : "+M_Player.player.foodCount);
        //delay for smoke
        if (!smoke.isPlaying && M_Player.player.gameOver)
        {
            // load the main game over scene
            SceneManager.LoadScene(2);
        }
    }
}
