using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : MonoBehaviour
{
    public Animator animator;

    public float atkCD = 1.7f;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (gameObject.GetComponent<FieldOfView>().canSeePlayer && gameObject.GetComponent<FieldOfView>().distanceToTarget >= 1.4f)
        {
            animator.SetBool("Moving", true);
        }
        else if (gameObject.GetComponent<FieldOfView>().canSeePlayer && gameObject.GetComponent<FieldOfView>().distanceToTarget < 1.4f)
        {
            animator.SetBool("Moving", false);
            Attack();
        }
        else
        {
            animator.SetBool("Moving", false);
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

   
}
