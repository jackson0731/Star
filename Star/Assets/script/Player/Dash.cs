using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dodgeSpeed = 10f;
    public float dodgeDuration = 0.3f; // 闪避持续时间
    private bool isDodging = false;

    public bool isWall;
    public LayerMask whatIsWall;

    void Update()
    {
        WallCheck();
        // 检测闪避触发条件
        if (Input.GetKeyDown(KeyCode.L) && !isDodging)
        {
            StartCoroutine(Dodge());
        }

        if (isWall)
        {
            GetComponent<CapsuleCollider>().enabled = true;
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
        isWall = Physics.Raycast(transform.position, Vector3.right, GetComponent<Player>().playerHeight * 0.5f, whatIsWall);
    }
}
