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
    public GameObject playerObject;
    public GameObject spearPrefab;
    public Transform shootTransform;
    bool shotSpear;
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
        if (currentState == EnemyStates.RUN)
        {
            Run();
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
        if (navAgent.velocity.sqrMagnitude > 0 && navAgent.velocity.sqrMagnitude < 3)
        {
            enemyAnimator.SetBool("Walk", true);
            enemyAnimator.SetBool("Run", false);
        }
        else if (navAgent.velocity.sqrMagnitude >= 3)
        {
            enemyAnimator.SetBool("Run", true);
            enemyAnimator.SetBool("Walk", false);
        }
        else
        {
            enemyAnimator.SetBool("Walk", false);
        }
        if (CheckDistance() < 25f)
        {
            if (CheckDistance() < 15f)
                currentState = EnemyStates.RUN;
            else
                currentState = EnemyStates.ATTACK; 
        }
        else
            currentState = EnemyStates.PATROL;
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
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        navAgent.SetDestination(new Vector3(navHit.position.x, navHit.position.y, playerObject.transform.position.z + 23.42f));
        transform.LookAt(playerObject.transform);
        if (CheckDistance() < 15f)
            currentState = EnemyStates.RUN;
        else
            currentState = EnemyStates.ATTACK;
        if (shotSpear)
            return;
        StartCoroutine(AttackAction());
    }

    IEnumerator AttackAction()
    {
        if (shotSpear)
            yield break;
        shotSpear = true;
        
        enemyAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        if (shotSpear)
        {
            GameObject spearClone = Instantiate(spearPrefab, shootTransform.position, shootTransform.rotation);
            yield return new WaitForSeconds(2f);
            Destroy(spearClone);
            yield return new WaitForSeconds(3f);
            shotSpear = false;
            enemyAnimator.ResetTrigger("Attack");
        }
        if (CheckDistance() >= 25f)
            currentState = EnemyStates.PATROL;
    }

    void Run()
    {
        Debug.Log("Inside Run");
        navAgent.speed = 10f;
        enemyAnimator.ResetTrigger("Attack");
        enemyAnimator.SetBool("Run", true);
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        navAgent.SetDestination(navHit.position);
        navAgent.SetDestination(new Vector3(navHit.position.x, navHit.position.y, playerObject.transform.position.z + 23.42f));
        if (CheckDistance() >= 15f)
        {
            enemyAnimator.SetBool("Run", false);
            if (CheckDistance() >= 25f)
                currentState = EnemyStates.PATROL;
            else
                currentState = EnemyStates.ATTACK;
        }
    }

    float CheckDistance()
    {
        return Vector3.Distance(transform.position, playerObject.transform.position);
    }
}
