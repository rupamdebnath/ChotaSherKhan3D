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
        }
        if ((transform.position.z - playerObject.transform.position.z) < 25f)
        {
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
        //navAgent.SetDestination(new Vector3(navAgent.transform.position.x, navAgent.transform.position.y, 23.42f + playerObject.transform.position.z));
        transform.LookAt(playerObject.transform);
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
        GameObject spearClone = Instantiate(spearPrefab, shootTransform.position, shootTransform.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(spearClone);
        yield return new WaitForSeconds(3f);
        shotSpear = false;
        if ((transform.position.z - playerObject.transform.position.z) >= 25f)
            currentState = EnemyStates.PATROL;
        else
            currentState = EnemyStates.ATTACK;
    }
}
