using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("Public Object")]
    public GameObject player;
    public GameObject body;
    public Animator animator;
    public Rigidbody rb;
    public GameObject spawn;
    private string currentScene;
    private GameObject VCamera;


    [Header("Public Value")]
    public float speed = 0.1f;
    public Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    public float jumpForce = 5.0f;
    public bool isOnGround;
    public int jumpCount;
    public bool jumpPress;
    public float hp = 100;
    public Slider hpSlider;

    float xVelocity;
    float climbSpeed = 5f;
    private State CurrentState;

    public static Player morePlayer { get; private set; }

    private enum State
    {
        Alive,
        Dead

    }

    private void Awake()
    {
        if (morePlayer != null)
        {
            Destroy(this.gameObject);
            return;
        }
        morePlayer = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawn = GameObject.Find("Spawn");
        currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        Move();
        Spawn();
        VCameraSet();
        hpLeft();
    }
    private void VCameraSet()
    {
        VCamera = GameObject.Find("CM vcam1");
        if (SceneManager.GetActiveScene().name != "2")
        {
            if (VCamera.GetComponent<CinemachineVirtualCamera>().Follow == null)
            {
                VCamera.GetComponent<CinemachineVirtualCamera>().Follow = this.transform;
            }
        }
    }
    private void Spawn()
    {
        if (currentScene == "MainMenu")
        {
            Destroy(this.gameObject);
        }

        if(currentScene != SceneManager.GetActiveScene().name)
        {
            if(SceneManager.GetActiveScene().name != "2")
            {
                if (spawn == null)
                {
                    spawn = GameObject.Find("Spawn");
                    gameObject.transform.position = spawn.transform.position;
                }
            }
            currentScene = SceneManager.GetActiveScene().name;
        }
    }

    private void Move()
    {
        
        if(CurrentState == State.Alive)
        {
            xVelocity = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector3(xVelocity * speed, rb.velocity.y, 0);

            if (Input.GetKey("d"))
            {
                animator.SetBool("Walking", true);
                player.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (Input.GetKey("a"))
            {
                animator.SetBool("Walking", true);
                player.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else
            {
                animator.SetBool("Walking", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 1)
            {
                animator.SetTrigger("1stJump");
                animator.SetBool("Jumping", true);
                jumpCount--;
            }
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0 && !isOnGround)
            {
                animator.SetTrigger("2ndJump");
                animator.SetBool("Jumping", true);
                jumpCount--;
            }
        }
        
    }

    private void hpLeft()
    {
        hpSlider.value = hp;
        if(hp <= 0)
        {
            hp = 0;
            CurrentState = State.Dead;
            animator.SetBool("Dead", true);
            Debug.Log("Player is dead");
        }
    }

    public void BeingHit(float Damage)
    {
        
        if(CurrentState == State.Alive)
        {
            hp = hp - Damage;
            animator.SetTrigger("BeingHit");
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (other.gameObject.CompareTag("CanDown"))
        {
            isOnGround = true;
            if (Input.GetKey("s"))
            {

                other.gameObject.GetComponent<StairDown>().TD = false;
            }
        }



        if (other.gameObject.CompareTag("Stair"))
        {
            isOnGround = true;
            if (Input.GetKey("w"))
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 climbMovement = new Vector3(0f, verticalInput * climbSpeed * Time.deltaTime, 0f);
                transform.Translate(climbMovement);
                other.gameObject.GetComponent<Stair>().StairTD = false;


            }
            else if (Input.GetKey("s"))
            {
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 climbMovement = new Vector3(0f, verticalInput * climbSpeed * Time.deltaTime, 0f);
                transform.Translate(climbMovement);
                other.gameObject.GetComponent<Stair>().StairTD = true;
            }
            else
            {
                other.gameObject.GetComponent<Stair>().StairTD = true;

            }
        }
        animator.SetBool("Jumping", false);
        animator.SetBool("InAir", false);
    }
    void OnTriggerExit(Collider other)
    {
        isOnGround = false;

        if (!jumpPress)
        {
            animator.SetBool("InAir", true);
        }

        if (other.gameObject.CompareTag("CanDown"))
        {

            other.gameObject.GetComponent<StairDown>().TD = true;
        }

        if (other.gameObject.CompareTag("Stair"))
        {

            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    public void Jump()
    {
        jumpPress = true;
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);

        if (jumpPress && isOnGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpPress = false;
        }
        else if (jumpPress && jumpCount >= 0 && !isOnGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpPress = false;
        }
    }

}
