using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int attackCount = 0;
    public float attackCD = 1.5f;
    public float attackSpeed = 0.5f;
    public float noCombo = 1.5f;
    public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCD -= Time.deltaTime;
        noCombo -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 0 && attackCD <= 0)
        {
            //第一段攻擊
            attackCount++;
            noCombo = 0.5f;
            
            ani.SetInteger("Attack", 1);
            
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 1 && noCombo >=0)
        {
            //第二段攻擊
            attackCount++;
            noCombo = 1f;

            ani.SetInteger("Attack", 2);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 2 && noCombo >= 0)
        {
            //第三段攻擊
            attackCD = 1.5f;
            noCombo = 0.5f;

            ani.SetInteger("Attack", 3);

        }
        else if(noCombo < 0)
        {
            attackCount = 0;
            ani.SetInteger("Attack", 0);
        }
    }
}
