using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject body;
    public float speed = 0.1f;
    public Animator animator;
    float xVelocity;

    public Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    public float jumpForce = 5.0f;
    int jumpCount;
    bool jumpPress;
    float climbSpeed = 5f;

    public bool isOnGround;
    private Rigidbody rb;

    public GameObject CanPass;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumpPress = true;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
        if (isOnGround)
        {
            jumpCount = 1;
        }
        if (jumpPress && isOnGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpCount--;
            jumpPress = false;
        }
        else if (jumpPress && jumpCount > 0 && !isOnGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpCount--;
            jumpPress = false;
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
        if (other.gameObject.CompareTag("Clear"))
        {
            SceneManager.LoadScene("2");
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
                if(gameObject.transform.position.y > other.gameObject.transform.parent.gameObject.transform.position.y)
                {
                    CanPass.GetComponent<MeshCollider>().enabled = true;
                }
                else
                {
                    CanPass.GetComponent<MeshCollider>().enabled = false;
                }
            }
        }

        if (other.gameObject.CompareTag("End"))
        {
            GameObject.Find("Boss").GetComponent<Rigidbody>().useGravity = true;
            if(GameObject.Find("Boss").transform.position.y <= gameObject.transform.position.y)
            {
                SceneManager.LoadScene("4");
            }
        }
        
        animator.SetBool("InAir", false);
    }
    void OnTriggerExit(Collider other)
    {
        isOnGround = false;
        animator.SetBool("InAir", true);
        if (other.gameObject.CompareTag("CanDown"))
        {
            CanPass.GetComponent<MeshCollider>().enabled = true;
        }
        if (other.gameObject.CompareTag("Stair"))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
