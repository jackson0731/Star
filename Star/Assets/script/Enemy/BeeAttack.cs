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
        if (gameObject.GetComponent<FieldOfView>().canSeePlayer)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("CAtk");
            atkCD = 1.7f;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Moving", false);
            atkCD -= Time.deltaTime;
            if (atkCD < 0)
            {
                atkCD = 1.7f;
            }
            if(atkCD == 1.7f)
            {
                animator.SetTrigger("CAtk");
            }
        }
    }
}
