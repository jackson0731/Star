using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int attackCount = 0;
    public float attackCD = 1.5f;
    public float attackSpeed = 0.5f;
    public float noCombo = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackCD -= Time.deltaTime;
        attackSpeed -= Time.deltaTime;
        noCombo -= Time.deltaTime;
        if (Input.GetKeyDown("f") && attackCount == 0 && attackCD <= 0)
        {
            //第一段攻擊
            attackCount++;
            attackSpeed = 0.5f;
            noCombo = 1.5f;
        }
        else if (Input.GetKeyDown("f") && attackCount == 1 && attackSpeed <= 0 && noCombo >=0)
        {
            //第二段攻擊
            attackCount++;
            attackSpeed = 0.5f;
            noCombo = 1.5f;
        }
        else if (Input.GetKeyDown("f") && attackCount == 2 && attackSpeed <= 0 && noCombo >= 0)
        {
            //第三段攻擊
            attackCD = 1.5f;
            attackCount = 0;
        }
        else if(noCombo < 0)
        {
            attackCount = 0;
        }
    }
}
