using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalZombie : ZombieAI
{
    public ActionStates state = ActionStates.Eat;
   [SerializeField] GameObject bitePoint;


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
        switch (state)
        {
            case ActionStates.Eat:
                meshAgent.SetDestination(bitePoint.transform.position);
                animator.SetBool("isDead", false);
                animator.SetBool("isWalk", true);

                if (meshAgent.remainingDistance < 1f)
                {
                    animator.SetBool("isWalk", false);
                }
                if (seePlayer.GetSeePlayer)
                {
                    state = ActionStates.Walk;
                }
                break;
            case ActionStates.Walk:
                meshAgent.SetDestination(playerRef.playerRef.transform.position);
                animator.SetBool("isWalk", true);

                if (!seePlayer.GetSeePlayer)
                {
                    state = ActionStates.Eat;
                }
                break;
            default:
                state = ActionStates.Eat;
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
