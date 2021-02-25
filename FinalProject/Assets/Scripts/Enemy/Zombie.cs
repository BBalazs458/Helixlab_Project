using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    public enum AIState { Idle,Chasing,Attack}

    public AIState myState = AIState.Idle;

    [SerializeField]  int health;
    [SerializeField]  float moveSpeed = 2.0f;
    [SerializeField]  float rotationSpeed = 10.0f;

    [SerializeField]  int takeDamage = 10;
    [SerializeField]  float attackRange = 2f;

    [SerializeField]  GameObject target;
    [SerializeField]  float detectionRange = 20f;
    [SerializeField]  float detectionAngel = 60f;

    Animator _animator;
    NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _agent.speed = moveSpeed;
        _agent.angularSpeed = rotationSpeed;
        _agent.stoppingDistance = 1;

        gameObject.name = "Zombie";
        target = GameObject.FindGameObjectWithTag("target");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeStates();
        Dead();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "knife")
        {
            health = 0;
        }
    }

    #region Methods
    public void Damage(int dmg)
    {
        health -= dmg;
        _animator.SetTrigger("isDamaged");
    }

    public void TakeDamage(int health)
    {
        health -= takeDamage;
    }
    public void Dead()
    {
        if (health <= 0)
        {
            _agent.enabled = false;
            _animator.SetBool("isDead", true);
            Destroy(gameObject,10f);
        }
    }
    void ChangeStates()
    {
        float distance = Vector3.Distance(target.transform.position, this.transform.position);
        switch (myState)
        {
            case AIState.Idle:
                //Play idle animation
                _animator.SetBool("isWalking",false);
                _animator.SetBool("isRunning",false);
                _animator.SetBool("isAttacking", false);
                //Animation end
                _agent.speed = 0;
                if (CanSeePlayer())
                {
                    myState = AIState.Chasing;
                }
                break;

            case AIState.Chasing:
                OnMoving(distance);
                break;

            case AIState.Attack:
                _agent.SetDestination(target.transform.position);
                _animator.SetBool("isAttacking", true);
                if (distance > attackRange)
                {
                    myState = AIState.Chasing;
                }
                break;
            default:
                break;
        }
    }
    void OnMoving(float distance)
    {
        if (CanSeePlayer())
        {
            _agent.SetDestination(target.transform.position);
            _agent.speed = moveSpeed;
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isAttacking", false);
            if (distance < attackRange)
            {
                myState = AIState.Attack;
            }
        }
        else
        {
            myState = AIState.Idle;
        }
    }

    bool CanSeePlayer()
    {
        //Target distance and angel calculation
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        float angel = Vector3.Angle(this.transform.forward, direction);

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hitInfo;
        
        //Debug.DrawRay(this.transform.position, target.transform.position, Color.red);
        if (Physics.Raycast(ray,out hitInfo,detectionRange))
        {
            if (hitInfo.collider.tag == "Player" && angel < detectionAngel)
            {
                if(hitInfo.collider.tag != "Enviroment")
                    return true;
            }
        }
        return false;
    }
    #endregion

}//class
