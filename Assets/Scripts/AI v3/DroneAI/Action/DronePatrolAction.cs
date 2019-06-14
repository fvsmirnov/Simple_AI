using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Drone/Actions/Patrol")]
public class DronePatrolAction : BaseAction
{
    public override void DoAction(BaseBrain brain)
    {
        Patrol(brain);
    }

    private void Patrol(BaseBrain brain)
    {
        brain.navMeshAgent.SetDestination(brain.patrolPoints[brain.nextPatrolPoint].position);

        if(brain.navMeshAgent.remainingDistance <= brain.navMeshAgent.stoppingDistance && !brain.navMeshAgent.pathPending)
            brain.nextPatrolPoint = (brain.nextPatrolPoint + 1) % brain.patrolPoints.Length;    //Clamp index between min and max array values
    }
}
