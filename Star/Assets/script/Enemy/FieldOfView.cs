
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Object")]
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    [SerializeField] private Light HeadLt;
    [SerializeField] private Transform[] PatrolPoints;
    [SerializeField] private int PDestination;

    [Header("Value")]
    public float radius;
    [Range(0,360)]
    public float angle;

    public bool canSeePlayer;
    public bool HasRoll;

    public float lostChase;
    public float lostPlayer;
    public float distanceToTarget;

    public ActionState ActState;

    private float RState;
    private Collider[] rangeChecks;
    private float StayTimer = 0;

    //public bool stop = true;

    public enum ActionState
    {
        Standy,
        Patrol,
        CloseCombat,
        LongRangeAttack,
    }

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(FOVRoutine());
        ActState = ActionState.Standy;
        lostPlayer = lostChase;
        HeadLt.color = new Color(0f, 176f, 255f);
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    HeadLt.color = new Color(255f, 0f, 0f);
                    lostPlayer = lostChase;
                    //Debug.Log("CanSeePlayer");
                }
                else
                {
                    //Debug.Log("CantSeePlayer");
                    StopChase();
                }
            }
            else
            {
                //Debug.Log("CantSeePlayer1");
                StopChase();
            }
        }
        else if (canSeePlayer)
        {
            //Debug.Log("CantSeePlayer2");
            StopChase();
        }
        
    }



    void Update()
    {
        FieldOfViewCheck();
        Action();
    }

    void Action()
    {
        Vector3 playerPosition = new Vector3(playerRef.transform.position.x, transform.position.y, 0);

        if (!canSeePlayer)
        {
            if (PDestination == 0)
            {
                
                ActState = ActionState.Patrol;
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[0].position, 1.5f * Time.deltaTime);

                if (Vector3.Distance(transform.position, PatrolPoints[0].position) < 0.2f)
                {
                    ActState = ActionState.Standy;
                    StayTimer += Time.deltaTime;
                    
                    if (StayTimer > 2)
                    {
                        PDestination = 1;
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                        StayTimer = 0;
                    }
                    
                }
            }

            if (PDestination == 1)
            {
                ActState = ActionState.Patrol;
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[1].position, 1.5f * Time.deltaTime);

                if (Vector3.Distance(transform.position, PatrolPoints[1].position) < 0.2f)
                {
                    ActState = ActionState.Standy;
                    StayTimer += Time.deltaTime;

                    if (StayTimer > 2)
                    {
                        PDestination = 0;
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                        StayTimer = 0;
                    }
                    
                }
            }
        }

        if(distanceToTarget > 10f && HasRoll == false && canSeePlayer)
        {
            RState = Random.Range(4, 10);
            HasRoll = true;
        }
        else if(distanceToTarget <= 10f && HasRoll == false && canSeePlayer)
        {
            RState = Random.Range(1, 3);
            HasRoll = true;
        }

        if(HasRoll == true)
        {
            if(RState > 3)
            {
                ActState = ActionState.LongRangeAttack;
                
            }
            else
            {
                if(canSeePlayer)
                {
                    if (playerRef.transform.position.x > transform.position.x && ActState != ActionState.CloseCombat)
                    {
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (playerRef.transform.position.x < transform.position.x && ActState != ActionState.CloseCombat)
                    {
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                    }

                    if (distanceToTarget > 1.4f)
                    {
                        ActState = ActionState.Patrol;
                        transform.position = Vector3.MoveTowards(transform.position, playerPosition, 1.5f * Time.deltaTime);
                        
                    }
                    else
                    {
                        ActState = ActionState.CloseCombat;

                    }

                }
                
            }

        }
        
    }

    private void StopChase()
    {
        if (canSeePlayer)
        {
            lostPlayer -= Time.deltaTime;
            if (lostPlayer < 0 && ActState!=ActionState.LongRangeAttack)
            {
                canSeePlayer = false;
                HasRoll = false;
                ActState = ActionState.Standy;
                lostPlayer = lostChase;
                HeadLt.color = new Color(0f, 176f, 255f);

                if(Vector3.Distance(transform.position, PatrolPoints[0].position) < Vector3.Distance(transform.position, PatrolPoints[1].position))
                {
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    PDestination = 0;
                }
                else if((Vector3.Distance(transform.position, PatrolPoints[0].position) > Vector3.Distance(transform.position, PatrolPoints[1].position)))
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    PDestination = 1;
                }
                else
                {
                    PDestination = Random.Range(0,1);
                }

            }

        }
        
    }

}
