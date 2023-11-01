using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dodgeSpeed = 10f;
    public float dodgeDuration = 0.3f; // 闪避持续时间
    [SerializeField] private bool isDodging = false;
    [SerializeField] private Animator Ani;
    public bool isWall;
    public LayerMask whatIsWall;
    private float t;
    private bool canDash;
    private GameObject dashUI;

    private void Start()
    {
        dashUI = GameObject.Find("Dash");
    }
    void Update()
    {
        WallCheck();
        CD();
        // 检测闪避触发条件
        if (Input.GetKeyDown(KeyCode.L) && !isDodging && canDash)
        {
            StartCoroutine(Dodge());
            t = 3f;
        }
    }

    IEnumerator Dodge()
    {
        // 开始闪避
        isDodging = true;
        // 提高移动速度或者应用其他闪避效果
        float originalSpeed = GetComponent<Player>().speed;
        GetComponent<Rigidbody>().useGravity = false;
        if (!isWall)
        {
            GetComponent<CapsuleCollider>().enabled = false;
        }
        GetComponent<Player>().speed = dodgeSpeed;
        Ani.SetTrigger("Dash");

        // 等待闪避持续时间
        yield return new WaitForSeconds(dodgeDuration);

        // 恢复玩家原始速度或者其他状态
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Player>().speed = originalSpeed;
        isDodging = false; // 结束闪避
    }

    public void WallCheck()
    {
        if (isWall)
        {
            GetComponent<CapsuleCollider>().enabled = true;
        }
        isWall = Physics.Raycast(transform.position, Vector3.right, GetComponent<Player>().playerHeight * 0.5f, whatIsWall);
        isWall = Physics.Raycast(transform.position, Vector3.left, GetComponent<Player>().playerHeight * 0.5f, whatIsWall);
    }
    public void CD()
    {
        t -= Time.deltaTime;
        if(t <= 0)
        {
            canDash = true;
            dashUI.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            canDash = false;
            dashUI.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
    }
}
