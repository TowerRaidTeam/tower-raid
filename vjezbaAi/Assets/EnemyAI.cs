using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    Transform player;
    Vector3 finalDestination;
    NavMeshAgent agent;
    public float closeCombatRange = 2f;
    public float rangeCombatDistance = 8f;
    public bool ranged;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        finalDestination = agent.destination;
    }

    private void Update()
    {
        finalDestination = player.position;
        agent.destination = finalDestination;
        

        //if (!ranged)
        //{
        //    if (Vector3.Distance(finalDestination, player.position) > closeCombatRange)
        //    {
        //        finalDestination = player.position;
        //        agent.destination = finalDestination;
        //    }
        //}
        //else
        //{
        //    if (Vector3.Distance(finalDestination, player.position) > rangeCombatDistance)
        //    {
        //        finalDestination = player.position;
        //        agent.destination = finalDestination;
        //    }
        //}
        

        
    }
}
