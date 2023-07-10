using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool justout;
    public float lostPlayer = 0.5f;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && lostPlayer != 0)
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
                    justout = true;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    void Update()
    {
        if(canSeePlayer == true || justout == true)
        {
            if(playerRef.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            transform.position = Vector3.MoveTowards(transform.position, playerRef.transform.position, 1f * Time.deltaTime);
        }
        if(canSeePlayer == false && justout == true)
        {
            lostPlayer -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerRef.transform.position, 1f * Time.deltaTime);
            if (lostPlayer <= 0f)
            {
                lostPlayer = 0.5f;
                justout = false;
            }
        }
    }
}
