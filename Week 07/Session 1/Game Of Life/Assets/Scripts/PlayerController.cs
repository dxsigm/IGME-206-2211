using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float xRange = 15.0f;
    public bool gameOver = false;

    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;
    public Camera theCamera;
    public GameObject food;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private Animator playerAnimator;
    private Rigidbody playerRigidbody;
    private AudioSource playerAudioSource;
    private AudioSource cameraAudioSource;
    private ScoreKeeper scoreKeeper;
    

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
        cameraAudioSource = theCamera.GetComponent<AudioSource>();
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        Debug.Log("Pizzas: " + scoreKeeper.nFood + " | Level: " + (scoreKeeper.level + 1) + " | Score: " + scoreKeeper.score);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < -xRange)
        {
            this.transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (this.transform.position.x > xRange)
        {
            this.transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.y < 0.1f)
        {
            playerAnimator.SetBool("Jump_b", false);
        }

        Vector3 cameraVector = new Vector3(this.transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z);
        Quaternion cameraQuaternion = theCamera.transform.rotation;
        theCamera.transform.SetPositionAndRotation(cameraVector, cameraQuaternion);

        if (!gameOver && transform.position.y < 0.1f)
        {
            /******************* Mouse INPUT *******************/
            Vector3 target = theCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
                theCamera.transform.position.z * -1));
            target = new Vector3(target.x, transform.position.y, transform.position.z);

            if (Mathf.Abs(target.x - transform.position.x) > 5)
            {
                transform.Translate((target - transform.position).normalized * Time.deltaTime * speed, Space.World);

                if (target.x > transform.position.x)
                {
                    transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                    dirtParticle.transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                }

                if (target.x < transform.position.x)
                {
                    transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                    dirtParticle.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                }

                dirtParticle.Play();
                playerAnimator.SetFloat("Speed_f", 5f);
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                dirtParticle.Stop();
                playerAnimator.SetFloat("Speed_f", 0);
            }

            if (transform.position.y < 0.1f && Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.AddForce(Vector3.up * 500);

                dirtParticle.Stop();
                playerAnimator.SetBool("Jump_b", true);
                playerAudioSource.PlayOneShot(jumpSound, 1);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (scoreKeeper.nFood > 0 && GameObject.FindGameObjectWithTag("Food") == null)
                {
                    --scoreKeeper.nFood;
                    Vector3 foodVector = new Vector3(transform.position.x, food.transform.position.y, transform.position.z);
                    Instantiate(food, foodVector, food.transform.rotation);

                    Debug.Log("Pizzas: " + scoreKeeper.nFood + " | Level: " + (scoreKeeper.level + 1) + " | Score: " + scoreKeeper.score);
                }
            }

            /******************* KEYBOARD INPUT *******************
            float horizontal = Input.GetAxis("Horizontal");

            if (horizontal < 0)
            {
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                dirtParticle.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                dirtParticle.Play();
                playerAnimator.SetFloat("Speed_f", 5);
            }
            else if (horizontal > 0)
            {
                transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                dirtParticle.transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                dirtParticle.Play();
                playerAnimator.SetFloat("Speed_f", 5);
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                dirtParticle.Stop();
                playerAnimator.SetFloat("Speed_f", 0);
            }

            this.transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed, Space.World);

            if (transform.position.y < 0.1f && Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerRigidbody.AddForce(Vector3.up * 500);

                dirtParticle.Stop();
                playerAnimator.SetBool("Jump_b", true);
                playerAudioSource.PlayOneShot(jumpSound, 1);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (scoreKeeper.nFood > 0 && GameObject.FindGameObjectWithTag("Food") == null)
                {
                    --scoreKeeper.nFood;
                    Vector3 foodVector = new Vector3(transform.position.x, food.transform.position.y, transform.position.z);
                    Instantiate(food, foodVector, food.transform.rotation);

                    Debug.Log("Pizzas: " + scoreKeeper.nFood + " | Level: " + (scoreKeeper.level + 1) + " | Score: " + scoreKeeper.score);
                }
            }
            ******************* KEYBOARD INPUT *******************/
        }

        if (gameOver)
        {
            if (!explosionParticle.isPlaying)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        
    }

    public void Die()
    {
        cameraAudioSource.Stop();
        dirtParticle.Stop();
        playerAnimator.SetBool("Death_b", true);
        playerAnimator.SetInteger("DeathType_int", 1);
        explosionParticle.Play();
        playerAudioSource.PlayOneShot(crashSound, 1f);
        gameOver = true;
    }
}
