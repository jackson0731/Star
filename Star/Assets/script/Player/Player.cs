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
    [SerializeField] Slider hpSlider;

    [Header("Public Value")]
    public float speed = 0.1f;
    [SerializeField] Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    public float jumpForce = 5.0f;
    [SerializeField] bool isOnGround;
    public int jumpCount;
    [SerializeField] bool jumpPress;
    public float hp = 100;
    public bool CanAss;
    public bool StateSwitch;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    float xVelocity;
    float climbSpeed = 5f;
    bool HasPlayedDeadAni;
    bool UsingComputer;
    private string currentScene;
    private GameObject VCamera;

    public static Player morePlayer { get; private set; }

    public State StateType;
    public enum State
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
        jumpCount = 2;
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
        GroundCheck();
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

        if (currentScene != SceneManager.GetActiveScene().name)
        {
            if (SceneManager.GetActiveScene().name != "2")
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

        if (CurrentState == LiveOrDie.Alive && StateType == State.CanMove)
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

            if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 1 && grounded)
            {
                animator.SetTrigger("1stJump");
                animator.SetBool("Jumping", true);
                jumpCount--;
            }
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0 && !grounded)
            {
                animator.SetTrigger("2ndJump");
                animator.SetBool("Jumping", true);
                jumpCount--;
            }
        }
        else if (CurrentState == LiveOrDie.Alive && StateType == State.Animation)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

    }

    private void hpLeft()
    {
        hpSlider.value = hp;
        if (hp <= 0)
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

        if (CurrentState == LiveOrDie.Alive)
        {
            hp = hp - Damage;
            animator.SetTrigger("BeingHit");
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("CanDown"))
        {
            grounded = true;
            if (Input.GetKey("s"))
            {

                other.gameObject.GetComponent<StairDown>().TD = false;
            }
        }

        if (other.gameObject.CompareTag("Stair"))
        {
            grounded = true;
            if (Input.GetKey("w"))
            {
                //Animation
                animator.SetBool("Walking", false);
                animator.SetBool("UsingLadder", true);
                animator.SetInteger("ClimbLadder", 1);
                player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                //Move_And_Collider
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 climbMovement = new Vector3(0f, verticalInput * climbSpeed * Time.deltaTime, 0f);
                transform.Translate(climbMovement);
                other.gameObject.GetComponent<Stair>().StairTD = false;


            }
            else if (Input.GetKey("s"))
            {
                //Animation
                animator.SetBool("Walking", false);
                animator.SetBool("UsingLadder", true);
                animator.SetInteger("ClimbLadder", -1);
                player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                //Move_And_Collider
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 climbMovement = new Vector3(0f, verticalInput * climbSpeed * Time.deltaTime, 0f);
                transform.Translate(climbMovement);
                other.gameObject.GetComponent<Stair>().StairTD = true;
            }
            else
            {
                other.gameObject.GetComponent<Stair>().StairTD = true;
                animator.SetInteger("ClimbLadder", 0);
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Input.GetKeyDown(KeyCode.F) && StateType == State.CanMove && CanAss == true)
            {
                Debug.Log("ASS");
                StateType = State.Animation;
                StateSwitch = true;
                animator.SetTrigger("Assing");

                FieldOfView FOV = other.GetComponent<FieldOfView>();
                Animator EnemyAni = other.GetComponent<Animator>();
                other.transform.position = transform.position;
                other.transform.rotation = transform.rotation;
                FOV.BeStab = true;
                EnemyAni.SetTrigger("BeAssed");
            }
        }

        if (other.gameObject.CompareTag("Computer"))
        {

            if (Input.GetKeyDown(KeyCode.F) && StateType == State.CanMove)
            {
                StateType = State.Animation;
                StateSwitch = true;
            }

            if (StateType == State.Animation && StateSwitch == true)
            {
                if (Vector3.Distance(transform.position, other.transform.position) > 0.1f)
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



        if (other.gameObject.CompareTag("CanDown"))
        {

            other.gameObject.GetComponent<StairDown>().TD = true;
        }

        if (other.gameObject.CompareTag("Stair"))
        {
            other.gameObject.GetComponent<Stair>().StairTD = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            animator.SetBool("UsingLadder", false);
            animator.SetInteger("ClimbLadder", 0);
        }
    }
    public void Jump()
    {
        jumpPress = true;
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);

        if (jumpPress && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpPress = false;
        }
        else if (jumpPress && jumpCount >= 0 && !grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpPress = false;
        }
    }

    public void GroundCheck()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);

        if (!jumpPress && !grounded)
        {
            animator.SetBool("InAir", true);
        }
        else
        {
            animator.SetBool("InAir", false);
        }
    }

    public void JumpReset()
    {
        jumpCount = 2;
    }
}
