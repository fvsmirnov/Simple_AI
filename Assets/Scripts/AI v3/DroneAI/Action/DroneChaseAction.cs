using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Drone/Actions/Chase")]
public class DroneChaseAction : BaseAction
{
    public override void DoAction(BaseBrain brain)
    {
        if (brain.Target == null)
            brain.ChangeState(typeof(BaseWanderState));

        brain.transform.LookAt(brain.Target.transform, Vector3.up);
        brain.navMeshAgent.SetDestination(brain.Target.transform.position);

        if (brain.navMeshAgent.remainingDistance <= brain.navMeshAgent.stoppingDistance)
        {
            brain.ChangeState(typeof(BaseAttackState));
        }   
    }
}
