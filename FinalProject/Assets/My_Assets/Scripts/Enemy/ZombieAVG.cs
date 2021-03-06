using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAVG : ZombieAI
{
    public ActionStates states = ActionStates.Idle;

    [SerializeField] GameObject[] wayPoints;
    int currentWP;

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

        //if (audioSource.isPlaying)
        //{
        //    audioSource.pitch = Time.timeScale;// pause menüben nem szól
        //}
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

    public override void Damage(int dmg)
    {
        health -= dmg;
    }
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

                PlayAudio(breathClip, true);//

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
                PlayAudio(breathClip, true);//
                Walking();
                break;

            case ActionStates.Run:
                PlayAudio(breathClip, true);//
                Run(TargetDistance(playerRef.PlayerReferenc.transform.position, this.transform.position));
                break;

            case ActionStates.Attack:
                PlayAudio(attackClip, true);//

                meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                animator.SetBool("isAttack", true);

                if (TargetDistance(playerRef.PlayerReferenc.transform.position, this.transform.position) > attackRange)
                {
                    StopAudio(attackClip);//
                    states = ActionStates.Run;
                }
                if (playerDead.GameOver == true)
                {
                    StopAudio(attackClip);//
                    states = ActionStates.Idle;
                }
                break;

            default:
                states = ActionStates.Idle;
                break;
        }
    }

    void Walking()
    {
        if (seePlayer.GetSeePlayer)
        {
            states = ActionStates.Run;
        }
        else
        {
            //Patrolling between points
            if (Vector3.Distance(wayPoints[currentWP].transform.position,
                                 this.transform.position) < 1.5f)
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

    void Run(float distance)
    {
        meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
        meshAgent.speed = runSpeed;
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", true);
        animator.SetBool("isAttack", false);
        if (!seePlayer.GetSeePlayer)
        {
            states = ActionStates.Idle;
        }
        else if (distance < attackRange)
        {
            StopAudio(breathClip);//
            states = ActionStates.Attack;
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
            StopAudio(breathClip);//
            PlayAudio(deathClip, false);//
            animator.SetBool("isDead", true);
            meshAgent.ResetPath();
            this.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 10f);
        }
    }
    #endregion
}//class
