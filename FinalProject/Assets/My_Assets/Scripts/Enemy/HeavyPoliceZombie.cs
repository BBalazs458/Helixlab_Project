using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyPoliceZombie : ZombieAI
{

    public ActionStates state = ActionStates.Idle;



    new void Start()
    {
        base.Start();
    }


    void FixedUpdate()
    {
        ChangeState();
        Dead();
        GetHealth();
    }

    #region METHODS
    protected override void ChangeState()
    {
        switch (state)
        {

            case ActionStates.Idle:
                PlayAudio(breathClip, true);

                meshAgent.speed = 0;
                animator.SetBool("isWalk", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isAttack", false);

                if (seePlayer.GetSeePlayer)
                {
                    state = ActionStates.Walk;
                }
                break;

            case ActionStates.Walk:
                PlayAudio(breathClip, true);
                Walking(TargetDistance(playerRef.PlayerReferenc.transform.position, this.transform.position));
                break;

            case ActionStates.Attack:
                PlayAudio(attackClip, true);

                meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                animator.SetBool("isAttack", true);

                if (TargetDistance(playerRef.PlayerReferenc.transform.position, this.transform.position) > attackRange)
                {
                    StopAudio(attackClip);
                    state = ActionStates.Walk;
                }
                if (playerDead.GameOver)
                {
                    StopAudio(attackClip);
                    state = ActionStates.Idle;
                }
                break;

            default:
                state = ActionStates.Idle;
                break;
        }
    }


    void Walking(float distance)
    {
        if (seePlayer.GetSeePlayer)
        {
            meshAgent.speed = moveSpeed;
            animator.SetBool("isWalk", true);
            animator.SetBool("isDead", false);
            animator.SetBool("isAttack", false);
            meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);

            if (distance < attackRange)
            {
                StopAudio(breathClip);
                state = ActionStates.Attack;
            }
        }
        else
        {
            state = ActionStates.Idle;
        }
    }


    public override void Damage(int dmg)
    {
        health -= dmg;
        healthBar.value = health;
    }

    public override float GetHealth()
    {
        healthBar.value = health;
        return healthBar.value;
    }

    protected override void Dead()
    {
        if (health <= 0)
        {
            StopAudio(breathClip);
            PlayAudio(deathClip, false);

            animator.SetBool("isDead", true);
            meshAgent.ResetPath();
            this.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 10f);
        }
    }
    #endregion

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            hit.GetComponent<PlayerStats>().TakeDamagePlayer(takeDamage);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "knife")
        {
            health -= 50;
        }
    }
}//class
