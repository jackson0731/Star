using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : MonoBehaviour
{
    public Animator animator;
    public Transform firePoint;
    public GameObject FireBall;

    [SerializeField]private float atkCD;
    [SerializeField]private float ChargeTime;
    private float LAtkTimer;

    public FieldOfView FOV;

    private LRAtkState LRAtk;

    private enum LRAtkState
    {
        Start,
        Charging,
        Fire,
    } 

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        LAtkTimer = ChargeTime;
    }

    void Update()
    {
        Action();
    }

    void Action()
    {
        if(FOV.ActState == FieldOfView.ActionState.Patrol)
        {
            animator.SetBool("Moving", true);
        }
        else if(FOV.ActState == FieldOfView.ActionState.Standy)
        {
            animator.SetBool("Moving", false);
        }

        if (FOV.ActState == FieldOfView.ActionState.CloseCombat)
        {
            animator.SetBool("Moving", false);
            Attack();
        }

        if (FOV.ActState == FieldOfView.ActionState.LongRangeAttack)
        {
            if (LRAtk == LRAtkState.Start)
            {
                animator.SetTrigger("StartCh");
                LRAtk = LRAtkState.Charging;
                
            }
            else if (LRAtk == LRAtkState.Charging)
            {
                animator.SetBool("Charging",true);
                LAtkTimer -= Time.deltaTime;
                if (LAtkTimer < 0)
                {
                    LRAtk = LRAtkState.Fire;
                    LAtkTimer = ChargeTime;
                }
                
            }
            else if (LRAtk == LRAtkState.Fire)
            {
                animator.SetBool("Charging", false);
                animator.SetTrigger("Fire");
                
                Shoot();
            }
        }

    }

    void Attack()
    {
        if (atkCD < 0)
        {
            animator.SetTrigger("CAtk");
            atkCD = 1.7f;
        }
        atkCD -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(FireBall, firePoint.position, firePoint.rotation);
        
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        
        bulletRigidbody.AddForce(firePoint.forward * 20, ForceMode.Impulse);
        
    }

    public void ResetState()
    {
        FOV.ActState = FieldOfView.ActionState.Standy;
        FOV.HasRoll = false;
        LRAtk = LRAtkState.Start;
    }
}
