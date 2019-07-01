using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/Actions/Chase", fileName = "CharacterChaseAction")]
public class CharacterChaseAction : BaseAction
{
    public override void DoAction(BaseBrain brain)
    {
        if (brain.Target == null)
        {
            brain.ChangeState(typeof(BaseWanderState));
            return;
        }

        brain.navMeshAgent.SetDestination(brain.Target.transform.position);

        if (brain.navMeshAgent.remainingDistance <= brain.navMeshAgent.stoppingDistance)
        {
            brain.ChangeState(typeof(BaseAttackState));
            return;
        }
    }
}
