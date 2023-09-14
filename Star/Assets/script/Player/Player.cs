using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class Player : MonoBehaviour
{
    [Header("Public Object")]
    public GameObject player;
    public GameObject body;
    public Animator animator;
    public GameObject CanPass;
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

    float xVelocity;
    float climbSpeed = 5f;

    public static Player morePlayer { get; private set; }

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

        if (CanPass == null && SceneManager.GetActiveScene().name != "2")
        {
            CanPass = GameObject.FindGameObjectWithTag("CanDown").transform.parent.gameObject;
        }
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
                
                CanPass.GetComponent<MeshCollider>().enabled = false;
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
                CanPass.GetComponent<MeshCollider>().enabled = false;
            }
            else if (Input.GetKey("s"))
            {
                CanPass.GetComponent<MeshCollider>().enabled = false;
            }
            else
            {
                CanPass.GetComponent<MeshCollider>().enabled = true;
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
            CanPass.GetComponent<MeshCollider>().enabled = true;
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
