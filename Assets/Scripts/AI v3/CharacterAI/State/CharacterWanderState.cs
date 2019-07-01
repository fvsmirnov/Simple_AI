using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/States/Wander", fileName = "CharacterWanderState")]
public class CharacterWanderState : BaseWanderState
{
    int waitOnPoint = 0;

    public override void Init(BaseBrain brain)
    {
        brain.SetSpeed(brain.normalSpeed);
        //brain.animator.SetTrigger("Patrol");

        brain.animator.SetBool("AttackB", false);
        brain.animator.SetBool("PrepareAttackB", false);
        brain.animator.SetBool("TargetLostB", false);
        brain.animator.SetBool("PatrolB", true);
    }
}
