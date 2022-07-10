using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyStates currentState;
    NavMeshAgent navAgent;
    Animator enemyAnimator;
    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f, walkSpeed = 1.0f;
    public float patrolForThisTime = 15f;
    float patrolTimer;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentState = EnemyStates.PATROL;
        patrolTimer = patrolForThisTime;
    }
    private void Update()
    {
        if(currentState == EnemyStates.PATROL)
        {
            Patrol();
        }
        if (currentState == EnemyStates.ATTACK)
        {
            Attack();
        }
    }

    private void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;        
        patrolTimer += Time.deltaTime;
        if(patrolTimer > patrolForThisTime)
        {            
            SetNewDestination();
            patrolTimer = 0f;
        }
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.SetBool("Walk", true);
        }
        else
        {
            enemyAnimator.SetBool("Walk", false);
            Debug.Log("WAlk is false");
        }
    }

    void SetNewDestination()
    {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        navAgent.SetDestination(navHit.position);
    }

    void Attack()
    {

    }
}
