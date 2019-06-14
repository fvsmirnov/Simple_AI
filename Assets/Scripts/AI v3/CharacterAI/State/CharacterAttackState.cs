using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/States/Attack", fileName = "CharacterAttackState")]
public class CharacterAttackState : BaseAttackState
{
    [Tooltip("Attack interval in seconds")]
    public float attackInterval = 3f;

    private IEnumerator enumerator;
    private float startCount;

    public override void Init(BaseBrain brain)
    {
        startCount = Time.realtimeSinceStartup;
        enumerator = AttackBehaviour(brain);
        brain.StartCoroutine(enumerator);
    }

    public override void Execute(BaseBrain brain) { }

    public override void Exit(BaseBrain brain)
    {
        Debug.Log("Exit");
        brain.changeStatePossible = true;
        brain.StopCoroutine(enumerator);
    }

    private int[] time = new int[2]{3, 3};
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
                //brain.StopCoroutine(action);
                brain.ChangeState(typeof(BaseChaseState));
            }

            if (executeNextAction)
            {
                action = ActWithDelay(brain, actions[actionId], state => executeNextAction = state, 0, time[actionId]);
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

    void StopAction(IEnumerator action, BaseBrain brain)
    {

    }

    /// <summary>
    /// Execute action with delay (before / after)
    /// </summary>
    /// <param name="callback">Return true when all operations executed</param>
    /// <param name="delayBefore">Delay before action will be executed</param>
    /// <param name="delayAfter">Delay after action was executed</param>
    /// <returns></returns>
    private IEnumerator ActWithDelay(BaseBrain brain, BaseAction action, Action<bool> callback, float delayBefore = 0, float delayAfter = 0)
    {
        if(delayBefore > 0)
            yield return new WaitForSeconds(delayBefore);

        action.DoAction(brain);

        if(delayAfter > 0)
            yield return new WaitForSeconds(delayAfter);

        yield return null;  //Need if delayBefore and delayAfter equals zero.
        callback(true);
    }
}