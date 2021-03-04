using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Detection Setting")]
    [Range(0,20)] 
    public float radius;
    [Range(0,180)]
    public float angel;

    public GameObject playerRef;
    [Header("Layer Setting")]
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public bool GetSeePlayer { get { return canSeePlayer; } }
    public GameObject PlayerReferenc { get { return playerRef; } }


    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        StartCoroutine(FOVRoutine());
    }

    IEnumerator FOVRoutine()
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
        Collider[] rangeChecks = Physics.OverlapSphere(this.transform.position, radius, targetMask);


        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - this.transform.position).normalized;

            if (Vector3.Angle(transform.forward,directionToTarget) < angel / 2)
            {
                float distanceToTarget = Vector3.Distance(this.transform.position, target.position);

                if (!Physics.Raycast(transform.position,directionToTarget,distanceToTarget,obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}
