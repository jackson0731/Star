using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int attackCount = 0;
    public float attackCD = 1.5f;
    public float noCombo = 1.5f;
    public Animator ani;
    public Player Player;
    public AniEvent AE;
    [SerializeField] private Collider AtkCollider;

    // Start is called before the first frame update
    void Start()
    {
        ani = GameObject.Find("Player").GetComponent<Animator>();
        Player = GameObject.Find("Player").GetComponent<Player>();
        AE = GameObject.Find("Player").GetComponent<AniEvent>();

        AtkCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        attackCD -= Time.deltaTime;
        noCombo -= Time.deltaTime;

        Atk();
        HitBoxSwitch();
    }

    void Atk()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 0 && attackCD <= 0)
        {
            //�Ĥ@�q����
            attackCount++;
            noCombo = 0.5f;
            Player.speed = 1.5f;

            ani.SetInteger("Attack", 1);
            ani.SetBool("IsAtk", true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 1 && noCombo >= 0)
        {
            //�ĤG�q����
            attackCount++;
            noCombo = 1f;

            ani.SetInteger("Attack", 2);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 2 && noCombo >= 0)
        {
            //�ĤT�q����
            attackCD = 1.25f;
            noCombo = 1f;
            attackCount = 0;

            ani.SetInteger("Attack", 3);
            ani.SetBool("IsAtk", false);
        }
        else if (noCombo < 0)
        {
            attackCount = 0;
            Player.speed = 5f;
            ani.SetInteger("Attack", 0);
            ani.SetBool("IsAtk", false);
        }
    }

    void HitBoxSwitch()
    {
        if (AE.IsAtk == true)
        {
            AtkCollider.enabled = true;
        }
        else if (AE.IsAtk == false)
        {
            AtkCollider.enabled = false;
        }
    }
}