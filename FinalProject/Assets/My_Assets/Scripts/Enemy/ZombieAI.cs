using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public abstract class ZombieAI : MonoBehaviour
{
    public enum ActionStates { Eat, Idle, Walk, Run, Attack }

    public Slider healthBar;

    public  AudioSource audioSource;
    public  AudioClip breathClip;
    public  AudioClip attackClip;
    public  AudioClip deathClip;

    public int health;
    public float moveSpeed;
    public float runSpeed;
    public float rotationSpeed;
    public float navMeshStopDistance;

    public int takeDamage;
    public float attackRange;

    protected Animator animator;
    protected NavMeshAgent meshAgent;
    protected FieldOfView seePlayer;
    protected FieldOfView playerRef;
    protected PlayerStats playerDead;


    protected void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("EnemyHealthBar").GetComponent<Slider>();
        //Debug.Log(healthBar);
        healthBar.maxValue = health;

        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        seePlayer = GetComponent<FieldOfView>();
        playerRef = GetComponent<FieldOfView>();
        playerDead = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        meshAgent.speed = moveSpeed;
        meshAgent.angularSpeed = rotationSpeed;
        meshAgent.stoppingDistance = navMeshStopDistance;
    }

    public abstract void Damage(int dmg);
    public abstract float GetHealth();
    protected abstract void Dead();
    protected abstract void ChangeState();


    public static float TargetDistance(Vector3 target,Vector3 thisNPC)
    {
        float distance = Vector3.Distance(target, thisNPC);
        return distance;
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

}//class
