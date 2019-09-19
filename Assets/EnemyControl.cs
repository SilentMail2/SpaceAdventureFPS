using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    enum state { patrol, flee, takecover, wander, attack, search, }

    public Transform[] patrolPoint;
    public int navNumber;
    public Vector3 targetPoint;
    public Transform enemy;
    enum States {Patrolling, Flee, Take}
    public NavMeshAgent agent;

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
        Patrol();
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
        //shoot at targetted enemies last location
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
    
}
