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
    
    [Header("Public Value")]
    public float speed = 0.1f;
    [SerializeField] Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    public float jumpForce = 5.0f;
    [SerializeField] bool isOnGround;
    public int jumpCount;
    [SerializeField] bool jumpPress;
    public float hp = 100;
    [SerializeField] Slider hpSlider;
    public bool StateSwitch;

    float xVelocity;
    float climbSpeed = 5f;
    bool HasPlayedDeadAni;
    bool UsingComputer;
    private string currentScene;
    private GameObject VCamera;

    public static Player morePlayer { get; private set; }

    private State StateType;
    private enum State
    {
        CanMove,
        Animation
    }

    private LiveOrDie CurrentState;
    private enum LiveOrDie
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
        
        if(CurrentState == LiveOrDie.Alive && StateType == State.CanMove)
        {
            xVelocity = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector3(xVelocity * speed, rb.velocity.y, 0);

            if (xVelocity == 1)
            {
                animator.SetBool("Walking", true);
                player.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (xVelocity == -1)
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
            CurrentState = LiveOrDie.Dead;
            if (!HasPlayedDeadAni)
            {
                animator.SetTrigger("Dead");
                HasPlayedDeadAni = true;
            }
            
            //Debug.Log("Player is dead");
        }
    }

    public void BeingHit(float Damage)
    {
        
        if(CurrentState == LiveOrDie.Alive)
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

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Input.GetKeyDown(KeyCode.F) && StateType == State.CanMove)
            {
                Debug.Log("ASS");
                //StateType = State.Animation;
                //StateSwitch = true;
            }
        }

        if (other.gameObject.CompareTag("Computer"))
        {
            
            if (Input.GetKeyDown(KeyCode.F) && StateType == State.CanMove)
            {
                StateType = State.Animation;
                StateSwitch = true;
            }

            if (StateType == State.Animation && StateSwitch== true)
            {
                if(Vector3.Distance(transform.position, other.transform.position) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);
                    transform.LookAt(other.transform.position);
                    animator.SetBool("Walking", true);
                    
                }
                else
                {
                    animator.SetBool("Walking", false);
                    player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (!UsingComputer)
                    {
                        animator.SetTrigger("Computer");
                        UsingComputer = true;
                    }
                    //Debug.Log("This is Computer");
                }
                
            }
            if (StateType == State.Animation && StateSwitch == false)
            {

                UsingComputer = false;
                Vector3 Return = new Vector3(transform.position.x, transform.position.y, 0);
                if (transform.position != Return)
                {
                    animator.SetBool("Walking", true);
                    transform.position = Vector3.MoveTowards(transform.position, Return, speed * Time.deltaTime);
                    transform.LookAt(Return);
                    
                }
                else
                {
                    animator.SetBool("Walking", false);
                    player.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                    StateType = State.CanMove;
                }

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
