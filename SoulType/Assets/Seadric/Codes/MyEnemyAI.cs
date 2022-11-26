using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyAI : MonoBehaviour
{

    public float lookRadius = 10;
    public float angle;

    public LayerMask walls;
    public LayerMask thePlayer;

    private float distance;

    private bool canSeePlayer;
    private bool isChasing = false;
    private bool canPatrol = true;

    Transform target;
    NavMeshAgent agent;
    Vector3 startingPos;

    // For patrol movement
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 someTarget;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        startingPos = transform.position;
        NewDestination();

    }

    void Update()
    {
        distance = Mathf.Abs(Vector3.Distance(target.position, transform.position));

        if (Mathf.Abs(Vector3.Distance(transform.position, someTarget)) < 2)
        {
            IterateWaypoints();
            NewDestination();
        }
        if (distance <= lookRadius)
        {
            FieldOfViewCheck(); // Check if player is visible
            if (canSeePlayer == true) // Chase if so
            {
                ChasePlayer();
                canPatrol = false; // Prevents patroling around
            }
            else if (canSeePlayer == false) // One the enemy loses sight of the player
            {
                // If the player was being chased, isChasing will be set true
                canPatrol = true;
                NewDestination();
            }
        }

        // If the player is outside the range of the enemy
        else if (distance > lookRadius)
        {
            agent.SetDestination(someTarget);
            if (Mathf.Abs(Vector3.Distance(transform.position, someTarget)) < 2)
            {
                IterateWaypoints();
                NewDestination();
            }
        }
    }

    // This method tracks the player and follows them
    void ChasePlayer()
    {
        isChasing = true;
        agent.SetDestination(target.position);
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }
    }

    // In the case the character is too close, the enemy stops rotating even if the player is moving
    // this method prevents the enemy from freezing up like that
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Just for testing and seeing radius range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    // Tells the enemy to move back to their original position
    void GoBack()
    {
        isChasing = false;
        agent.SetDestination(transform.position); // before going back, stand still
        StartCoroutine(CoUpdate());
        IEnumerator CoUpdate()
        {

            yield return new WaitForSeconds(2);
            agent.SetDestination(startingPos);
            yield return null;
        }
        canPatrol = true;

    }

    // Runs a raycast check to find the player layer
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, lookRadius, thePlayer);

        if (rangeChecks.Length != 0)
        {
            Transform someTarget = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, walls))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    void NewDestination()
    {
        someTarget = waypoints[waypointIndex].position;
        agent.SetDestination(someTarget);
    }


    void IterateWaypoints()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

}