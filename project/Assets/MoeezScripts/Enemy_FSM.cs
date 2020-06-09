using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_FSM : MonoBehaviour
{
    private Animator anim;
    private checkmyvision checkMyVision;
    private NavMeshAgent agent;
    private Transform playerTransform;
    private float attackD=1.3f;
    private Transform patrolDestination;

    private Health playerHealth;

    public float maxDamage = 0.00004f;
    
    // Enums to keep states
    public enum ENEMY_STATES { patrol, chase, attack }


    // We need a property to get the current state
    [SerializeField]
    private ENEMY_STATES currentState;

    public ENEMY_STATES CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            StopAllCoroutines();
            switch (currentState)
            {
                case ENEMY_STATES.patrol:
                    StartCoroutine(EnemyPatrol());
                    break;
                case ENEMY_STATES.chase:
                    StartCoroutine(EnemyChase());
                    break;
                case ENEMY_STATES.attack:
                    StartCoroutine(EnemyAttack());
                    break;
            }
        }
    }

    private void Awake()
    {
        checkMyVision = GetComponent<checkmyvision>();
        agent = GetComponent<NavMeshAgent>();
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerTransform = playerHealth.GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Dest");
        int pathIndex = Random.Range(0, destinations.Length);
        patrolDestination = destinations[Random.Range(0, destinations.Length)].GetComponent<Transform>();
        //  print($"Path: {pathIndex}");
        agent.stoppingDistance = 3;
        CurrentState = ENEMY_STATES.patrol;

    }

    public IEnumerator EnemyPatrol()
    {
        
        while (currentState == ENEMY_STATES.patrol)
        {

            checkMyVision.senstivity = checkmyvision.ensenstivity.HIGH;
            agent.isStopped = false;
            agent.SetDestination(patrolDestination.position);
            while (agent.pathPending)
            {

                yield return null;
            }
            if (checkMyVision.targetInSight)
            {
                agent.isStopped = true;
                CurrentState = ENEMY_STATES.chase;
                yield break;
            }

            yield return null;
        }

    }
    public IEnumerator EnemyChase()
    {
        
        while (currentState == ENEMY_STATES.chase)
        {
            checkMyVision.senstivity = checkmyvision.ensenstivity.LOW;
            // agent.acceleration = 600;

            agent.isStopped = false;
            bool destSet = agent.SetDestination(checkMyVision.lastKnownLocation);
            // print("Dest Set: " + destSet);
            while (agent.pathPending && agent.path.status == NavMeshPathStatus.PathInvalid)
            {
                print("Path Invalid: " + (agent.path.status == NavMeshPathStatus.PathInvalid));
                yield return null;
            }
            // print("Agent Remaining Distance: " + agent.remainingDistance);
            //  print("Agent Stopping Distance: " + agent.stoppingDistance);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                   
                    CurrentState = ENEMY_STATES.attack;
                
                yield break;
            }
            yield return null;
        }

    }
    public IEnumerator EnemyAttack()
    {
        
        while (currentState == ENEMY_STATES.attack)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
            while (agent.pathPending)
            {
                yield return null;
            }
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                
                if (anim.GetBool("stop") == true)
                {
                    anim.SetBool("stop", false);
                }
                CurrentState = ENEMY_STATES.patrol;
                yield break;
            }
            else
            {
                // Do something
                anim.SetBool("stop", true);
                playerHealth.HealthPoints -= maxDamage ;
                if(playerHealth.HealthPoints==0)
                yield break;
               
            }
            yield return new WaitForSeconds(1);
        }

        yield return null;
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
