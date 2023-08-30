using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int attackCount = 0;
    public float attackCD = 1.5f;
    public float noCombo = 1.5f;
    [SerializeField] private bool HasDealDamage;
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
        HasDealDamage = false;
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 0 && attackCD <= 0 || Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 3 && attackCD <= 0)
        {
            //第一段攻擊
            attackCount =1;
            noCombo = 0.5f;
            Player.speed = 1.5f;
            AE.IsAtk = true;

            ani.SetInteger("Attack", 1);
            ani.SetBool("IsAtk", true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 1 && noCombo >= 0)
        {
            //第二段攻擊
            attackCount=2;
            noCombo = 1f;

            ani.SetInteger("Attack", 2);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount == 2 && noCombo >= 0)
        {
            //第三段攻擊
            attackCD = 1.25f;
            noCombo = 1.5f;
            attackCount = 3;

            ani.SetInteger("Attack", 3);
        }
        else if (noCombo < 0)
        {
            attackCount = 0;
            Player.speed = 5f;
            ani.SetInteger("Attack", 0);
            ani.SetBool("IsAtk", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            HasDealDamage = true;
            other.gameObject.GetComponent<Enemy>().TakeDamage();
            
        }

    }

    void HitBoxSwitch()
    {
        if (AE.CanDealDamage == true)
        {
            AtkCollider.enabled = true;
        }
        else if (AE.CanDealDamage == false)
        {
            AtkCollider.enabled = false;
            HasDealDamage = false;
        }
    }
}
