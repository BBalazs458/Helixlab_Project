using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficerZombie : ZombieAI, IEnemyAudioManager
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
            health = 50;
        }
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
                PlayAudio(breathClip, true);
                meshAgent.speed = 0;
                animator.SetBool("isRun", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isAttack", false);
                if (seePlayer.GetSeePlayer)
                {
                    StopAudio(breathClip);
                    states = ActionStates.Run;
                }
                break;

            case ActionStates.Run:
                PlayAudio(breathClip, true);
                meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                animator.SetBool("isRun", true);
                animator.SetBool("isAttack", false);
                meshAgent.speed = runSpeed;
                if (TargetDistance(playerRef.PlayerReferenc.transform.position,this.transform.position) < attackRange)
                {
                    StopAudio(breathClip);
                    states = ActionStates.Attack;
                }
                if (!seePlayer.GetSeePlayer)
                {
                    states = ActionStates.Idle;
                }
                break;

            case ActionStates.Attack:
                PlayAudio(attackClip, true);
                meshAgent.SetDestination(playerRef.PlayerReferenc.transform.position);
                animator.SetBool("isAttack", true);
                if (TargetDistance(playerRef.PlayerReferenc.transform.position, this.transform.position) > attackRange)
                {
                    StopAudio(attackClip);
                    states = ActionStates.Run;
                }
                if (playerDead.GameOver)
                {
                    StopAudio(attackClip);
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
            StopAudio(breathClip);
            PlayAudio(deathClip, false);

            animator.SetBool("isDead", true);
            meshAgent.ResetPath();
            this.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 10f);
        }
    }

    public void PlayAudio(AudioClip clip, bool loop)
    {
        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void StopAudio(AudioClip clip)
    {
        if (audioSource.clip == clip)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
    #endregion
}//class
