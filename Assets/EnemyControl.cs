using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    enum state { patrol, flee, takecover, wander, attack, search, }
   [SerializeField] state behaviour;

    public Transform[] patrolPoint;
    public int navNumber;
    public Vector3 targetPoint;
    public Transform enemy;
    public NavMeshAgent agent;
    public Transform target;
    public float lookRadius = 10;
    public float timetoNextshot = 10;
    public float dam;
/*    public static Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }*/

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


           switch (behaviour)
            {
            case state.patrol:
                Patrol();
                print("On Patrol");
                break;
            case state.attack:
                Attack();
                print("attacking player");
                break;
            case state.flee:
                print("retreating");
                break;
            case state.search:
                print("searching for player");
                break;
            case state.takecover:
                print("Taking Cover");
                break;
            case state.wander:
                print("taking a look around");
                break;
            default:
                print("nothing");
                break;

            }

    }

    void Patrol()
    {
        //Walk between marked locations
        Debug.Log("Patrol Step1");
        int i = 0;
        if (i == navNumber)
        {
            Debug.Log("Patrol Step2");
            if (i == navNumber)
            {
                Debug.Log("Patrol Step3");

                agent.destination = patrolPoint[navNumber].position;
                
            }
        }
        if (this.transform.position.x == patrolPoint[i].position.x && this.transform.position.y == patrolPoint[i].position.y)
        {
            Debug.Log("Patrol Step4");
            navNumber++;
            i++;
        }
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            behaviour = state.attack;
        }

        Debug.Log("Patrol Step5");
    }
    void Flee()
    {
        //if health is low, and has low bravery, will flee away from player

    }
    void TakeCover()
    {
        //when cover is found and is close, go to the cover opposite side
    }
    void Wander()
    {
        //go to random point close to original location
    }
    void PickRandomLocation()
    {
       
    }
    void Attack()
    {
        agent.SetDestination(target.position);
        agent.stoppingDistance = 10;
        float distance = Vector3.Distance(target.position, transform.position);

        timetoNextshot -= 1;



        //shoot at targetted enemies last location
        float maxRange = 5;
        float randomDir = Random.Range(-0.5f, 0.5f);
        RaycastHit hit;
        if (timetoNextshot <= 0) {
            if (Vector3.Distance(transform.position, target.position + new Vector3(randomDir, randomDir, randomDir)) < maxRange)
            {
                if (Physics.Raycast(transform.position, (target.position - transform.position + new Vector3(randomDir, randomDir, randomDir)), out hit, maxRange))
                {
                    if (hit.transform == target)
                    {
                        Debug.DrawRay(transform.position, (target.position - transform.position + new Vector3(randomDir, randomDir, randomDir)), Color.green, maxRange);
                        timetoNextshot = 10;
                        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
                        playerHealth.TakeHealth(dam);
                        print("shot hit");
                    }
                    else if (hit.transform != target)
                    {
                        timetoNextshot = 10;
                        Debug.DrawRay(transform.position, (target.position - transform.position + new Vector3(randomDir, randomDir, randomDir)), Color.red);
                        print("missed");
                    }
                }
            }
        }
    }
    void SearchForPlayer ()
    {
        //if attacking and enemy isn't seen for a while, go to player's last known location, and if not found, wanders around searching for player
    }
    void TurnHostile()
    {
        // if not hostile, and player attacks them, or notices attacking one of their allies, turns hostile and attacks player
    }
    void FindCover()
    {
        //If unshield or has no shield searches the immediate area for cover facing away from the player
    }
    void Dodge()
    {
        //if notices grenade, jumps away from it
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
