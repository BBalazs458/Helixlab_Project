using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGirl : ZombieAI
{
    [SerializeField] GameObject[] wayPoints;
    int currentWP;

    public ActionStates states = ActionStates.Idle;

    new void Start()
    {
        base.Start();
        if (wayPoints.Length == 0) return;
        currentWP = Random.Range(0, wayPoints.Length);
    }


    void FixedUpdate()
    {
        ChangeState();
        Dead();
        GetHealth();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "knife")
        {
            health = 0;
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            hit.GetComponent<PlayerStats>().TakeDamagePlayer(takeDamage);
        }
    }
    #region METHODS

    public override float GetHealth()
    {
        healthBar.value = health;
        return healthBar.value;
    }

    protected override void ChangeState()
    {
        switch (states)
        {

            case ActionStates.Idle:
                animator.SetBool("isWalk", false);
                animator.SetBool("isRun", false);
                animator.SetBool("isAttack", false);

                if (seePlayer.GetSeePlayer)
                {
                    states = ActionStates.Run;
                }
                else
                {
                    StartCoroutine(WaitAndWalk());
                }
                break;
            case ActionStates.Walk:
                Patrol();
                break;
            case ActionStates.Run:
                meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                animator.SetBool("isRun", true);
                meshAgent.speed = runSpeed;
                if (!seePlayer.GetSeePlayer)
                {
                    states = ActionStates.Idle;
                }
                if (TargetDistance(playerRef.PlayerReferenc.transform.position, this.transform.position) < attackRange)
                {
                    states = ActionStates.Attack;
                }
                break;
            case ActionStates.Attack:
                if (TargetDistance(playerRef.PlayerReferenc.transform.position, this.transform.position) > attackRange && seePlayer.GetSeePlayer)
                {
                    meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                    animator.SetBool("isAttack", false);
                    animator.SetBool("isWalk", true);
                }
                else
                {
                    meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                    animator.SetBool("isAttack", true);
                }
                if (playerDead.GameOver)
                {
                    states = ActionStates.Idle;
                }
                break;
            default:
                states = ActionStates.Idle;
                break;
        }
    }

    void Patrol()
    {
        if (seePlayer.GetSeePlayer)
        {
            states = ActionStates.Run;
        }
        else
        {
            //Patrolling between points
            if (Vector3.Distance(wayPoints[currentWP].transform.position,
                                 this.transform.position) < 1)
            {
                currentWP++;
                if (currentWP >= wayPoints.Length)
                {
                    currentWP = 0;
                }
            }
            meshAgent.SetDestination(wayPoints[currentWP].transform.position);
            meshAgent.speed = moveSpeed;
            animator.SetBool("isWalk", true);
            animator.SetBool("isRun", false);
        }
    }

    IEnumerator WaitAndWalk()
    {
        yield return new WaitForSeconds(10f);
        states = ActionStates.Walk;
    }

    protected override void Dead()
    {
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            meshAgent.ResetPath();
            this.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 10f);
        }
    }

    public override void Damage(int dmg)
    {
        health -= dmg;
        animator.SetTrigger("Hit");
    }

    #endregion
}//class
