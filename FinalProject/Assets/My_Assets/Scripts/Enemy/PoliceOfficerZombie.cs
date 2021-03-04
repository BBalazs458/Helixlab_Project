using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficerZombie : ZombieAI
{
    public ActionStates states = ActionStates.Idle;


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
    public override void Damage(int dmg)
    {
        health -= dmg;
        healthBar.value = health;
        animator.SetTrigger("Hit");
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
                animator.SetBool("isRun", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isAttack", false);
                if (seePlayer.GetSeePlayer)
                {
                    states = ActionStates.Run;
                }
                break;
            case ActionStates.Run:
                meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                animator.SetBool("isRun", true);
                animator.SetBool("isAttack", false);
                meshAgent.speed = runSpeed;
                if (TargetDistance(playerRef.PlayerReferenc.transform.position,this.transform.position) < attackRange)
                {
                    states = ActionStates.Attack;
                }
                if (!seePlayer.GetSeePlayer)
                {
                    states = ActionStates.Idle;
                }
                break;
            case ActionStates.Attack:
                meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                animator.SetBool("isAttack", true);
                if (TargetDistance(playerRef.PlayerReferenc.transform.position, this.transform.position) > attackRange)
                {
                    states = ActionStates.Run;
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
    #endregion
}//class
