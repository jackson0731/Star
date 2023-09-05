using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : MonoBehaviour
{
    public Animator animator;
    public Transform firePoint;
    public GameObject FireBall;
    public LineRenderer Beam;

    [SerializeField]private float atkCD;
    [SerializeField] private float ChargeTime;

    bool UseBeam;
    float BeamRange = 5;

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
        Beam.enabled = false;
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

                if (Beam.enabled)
                {
                    Shoot();
                }

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
        Beam.SetPosition(0, firePoint.position);

        Vector3 Rayorigin = firePoint.position;
        RaycastHit Hit;
        if (Physics.Raycast(Rayorigin, firePoint.forward, out Hit))
        {
            
            Beam.SetPosition(1, Hit.point);
            //Debug.Log(Hit.collider.tag);
        }
        else
        {
            Beam.SetPosition(1, firePoint.position + (firePoint.transform.forward*7));
            //Debug.Log("2nd");
        }
    }

    public void BeamAtk()
    {
        UseBeam = true;
        Beam.enabled = true;
    }

    public void BeamAtkEnd()
    {
        UseBeam = false;
        Beam.enabled = false;
    }

    public void ResetState()
    {
        FOV.ActState = FieldOfView.ActionState.Standy;
        FOV.HasRoll = false;
        LRAtk = LRAtkState.Start;
    }
}
