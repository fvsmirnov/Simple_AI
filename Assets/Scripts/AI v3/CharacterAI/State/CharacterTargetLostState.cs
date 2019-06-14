using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/States/TargetLost", fileName = "CharacterTargetLostState")]
public class CharacterTargetLostState : BaseTargetLostState
{
    public override void Init(BaseBrain brain)
    {
        brain.SetSpeed(0);
        TargetLostAnimation(brain);
        brain.DoWhile(5, null, () => brain.ChangeState(typeof(BaseWanderState)));
    }

    void TargetLostAnimation(BaseBrain brain)
    {
        brain.animator.SetBool("PatrolB", false);
        brain.animator.SetBool("AttackB", false);
        brain.animator.SetBool("PrepareAttackB", false);
        brain.animator.SetBool("TargetLostB", true);
    }
}
