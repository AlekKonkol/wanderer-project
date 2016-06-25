using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]

public class Player : MonoBehaviour
{
    public bool playerNearTerminal;
    public bool playerNearTerminal2;
    public bool playerNearTerminal3;
    public bool playerNearTutTerminal1;
    public bool playerNearTutTerminal1b;
    public bool playerNearTutTerminal2;
    public bool playerInCam1;
    public bool playerInCam2;
    public bool playerInOldMan;
    public bool playerInRadio0;
    public bool playerAttack;
    public bool playerLRAttack;
    public bool noPlayerAttack;
    public bool noPlayerLRAttack;
    public bool activateOldManSpeak;
    public bool activateOldManSpeakNo;

    public GameObject Pit;
    public GameObject Term1;   
    public GameObject Term2;
    public GameObject Term3;
    public GameObject endLevel;
    public GameObject TutTerminal1;
    public GameObject TutTerminal1b;
    public GameObject TutTerminal2;
    public GameObject oldMan;

    public GameObject HUD;

    public AudioClip[] audioClip;

    public int sceneCountInBuildSettings;

    public float jumpHeight = 7;
    public float timeToJumpApex = .3f;
    public float moveSpeed = 6;

    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;

    float gravity;

    float jumpVelocity;

    float velocityXSmoothing;

    Vector3 velocity;

    Controller2D controller;

    private Collider2D myCollider;
    private Animator anim;
    private activateHUD enHUD;

    void Start()
    {
        //mySprite = GetComponent<SpriteRenderer>();
        //enHUD = HUD.GetComponent<activateHUD>();
        anim = GetComponent<Animator>();
        controller = GetComponent<Controller2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump Velocity " + jumpVelocity);

        //playerTransform = GameObject.Find("Player").transform;
        //Vector2 playerPos = playerTransform.position;
    }

    void Update()
    {
        //Debug.Log("Player is at: " + playerTransform);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (velocity.x > 0)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("idle", false);
        }

        if (velocity.x == 0)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Jump", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("Idle", true);
        }

        if (velocity.x < 0)
        {
            anim.SetBool("WalkLeft", true);
            anim.SetBool("Walk", false);
            anim.SetBool("idle", false);
        }

        if (velocity.y > 0)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("idle", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("Walk", false);
        }

        if (velocity.y <= 0)
        {
            anim.SetBool("Jump", false);
        }

        if (Input.GetKeyDown("r"))
        {
            PlaySound(0);
            AudioListener.pause = false;
        }

        if (Input.GetKeyDown("t"))
        {
            AudioListener.pause = true;
        }

        if ((playerNearTerminal == true) && (Input.GetKeyDown("e")))
        {
            SceneManager.LoadScene(Random.Range(1, 4));
        }

        if ((playerNearTerminal2 == true) && (Input.GetKeyDown("e")))
        {
            SceneManager.LoadScene(Random.Range(4, 7));
        }

        if ((playerNearTerminal3 == true) && (Input.GetKeyDown("e")))
        {
            SceneManager.LoadScene(Random.Range(7, 10));
        }

        if ((playerNearTutTerminal1 == true) && (Input.GetKeyDown("e")))
        {
            SceneManager.LoadScene("TutTerm1");
        }

        if (playerNearTutTerminal1 == true) {
            Debug.Log("near terminal 1");
        }

        if ((playerNearTutTerminal2 == true) && (Input.GetKeyDown("e")))
        {
            SceneManager.LoadScene("TutTerm2");
        }

        if ((playerNearTutTerminal1b == true) && (Input.GetKeyDown("e")))
        {
            SceneManager.LoadScene("TutTerm1b");
        }

        if ((playerInOldMan == true) && (Input.GetKeyDown("e")))
        {
            activateOldManSpeak = true;
        }

        if ((playerInOldMan == false) && (Input.GetKeyDown("e")))
        {
            activateOldManSpeakNo = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            playerAttack = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            playerAttack = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            noPlayerAttack = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            noPlayerAttack = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            playerLRAttack = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            playerLRAttack = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            noPlayerLRAttack = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            noPlayerLRAttack = false;
        }

        // if ((playerInOldMan == false))
        // {
        //      enHUD.DisableHUD();
        //  }
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		if (col.gameObject.name == "endLevel")
		{
			SceneManager.LoadScene("TBC");
		}

        if (col.gameObject.name == "Pit")
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TerminalBound")
        {
            playerNearTerminal = false;
            playerNearTerminal2 = false;
            playerNearTerminal3 = false;
            playerNearTutTerminal1 = false;
            playerNearTutTerminal2 = false;
            playerNearTutTerminal1b = false;
            Debug.Log("Player not in tut term 1");
        }

        if (col.gameObject.tag == "Terminal")
        {
            playerNearTerminal = true;
            playerInOldMan = false;
        }

        if (col.gameObject.tag == "Terminal2")
        {
            playerNearTerminal2 = true;
            playerInOldMan = false;
        }

        if (col.gameObject.tag == "Terminal3")
        {
            playerNearTerminal3 = true;
            playerInOldMan = false;
        }

        if (col.gameObject.name == "TutTerminal1")
        {
            playerNearTutTerminal1 = true;
            Debug.Log("Player in tut term 1");
        }

        if (col.gameObject.name == "TutTerminal1b")
        {
            playerNearTutTerminal1b = true;
            playerInOldMan = false;
        }

        if (col.gameObject.name == "TutTerminal2")
        {
            playerNearTutTerminal2 = true;
            playerInOldMan = false;
        }

        if (col.gameObject.name == "OldMan")
        {
            playerInOldMan = true;
        }

        if (col.gameObject.tag == "notOldMan")
        {
            playerInOldMan = false;
        }

        if (col.gameObject.name == "AudioTest")
        {
            playerInRadio0 = true;
        }
    }
}