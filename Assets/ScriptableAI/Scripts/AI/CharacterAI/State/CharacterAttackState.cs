using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/States/Attack", fileName = "CharacterAttackState")]
public class CharacterAttackState : BaseAttackState
{
    [Tooltip("Attack interval in seconds")]
    private float prepareAttackTime = 3f;
    private float attackTime = 3f;
    private IEnumerator attackBehaviour;
    private float[] attackInterval;  //time prepare attack, attack

    public override void Init(BaseBrain brain)
    {
        attackInterval = new float[] { prepareAttackTime, attackTime};
        attackBehaviour = AttackBehaviour(brain);
        brain.StartCoroutine(attackBehaviour);
    }

    public override void Execute(BaseBrain brain) { }

    public override void Exit(BaseBrain brain)
    {
        brain.changeStatePossible = true;
        brain.StopCoroutine(attackBehaviour);
    }

    private IEnumerator AttackBehaviour(BaseBrain brain)
    {
        int actionId = 0;
        IEnumerator action = null;
        bool executeNextAction = true;

        while (brain.Target != null)
        {
            //Check distance
            if (Vector3.Distance(brain.transform.position, brain.Target.transform.position) > brain.navMeshAgent.stoppingDistance)
            {
                brain.ChangeState(typeof(BaseChaseState));
            }

            if (executeNextAction)
            {
                action = action.ActWithDelay(() => actions[actionId].DoAction(brain),  //execute next action
                                                   state => executeNextAction = state, //Check if execution complete
                                                   0,                                  //time delay before
                                                   attackInterval[actionId]);          //time delay after

                brain.StartCoroutine(action);
                actionId = (actionId + 1) % actions.Length;
                executeNextAction = false;
            }
            yield return null;
        }

        brain.StopCoroutine(action);
        brain.changeStatePossible = true;
        yield return null;

        brain.ChangeState(typeof(BaseTargetLostState));
    }
}