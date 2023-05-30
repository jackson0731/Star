using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject body;
    public float speed = 0.1f;
    public Animator animator;
    float xVelocity;

    public Vector3 jump;
    public float jumpForce = 5.0f;
    int jumpCount;
    bool jumpPress;

    public bool isOnGround;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
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
            rb.velocity = new Vector3(rb.velocity.x, jumpForce,0);
            jumpCount--;
            jumpPress = false;
        }
        else if (jumpPress && jumpCount > 0 && !isOnGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce,0);
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
        }
    }
    void OnTriggerExit(Collider other)
    {
        isOnGround = false;
    }
}
