using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/States/TargetLost", fileName = "CharacterTargetLostState")]
public class CharacterTargetLostState : BaseTargetLostState
{
    IEnumerator enumerator;

    public override void Init(BaseBrain brain)
    {
        brain.SetSpeed(0);
        TargetLostAnimation(brain);

        enumerator = enumerator.DoWhile(5, null, () => brain.ChangeState(typeof(BaseWanderState)));
        brain.StartCoroutine(enumerator);
    }

    public override void Exit(BaseBrain brain)
    {
        brain.StopCoroutine(enumerator);
    }

    void TargetLostAnimation(BaseBrain brain)
    {
        brain.ResetAnimatorState();
        brain.animator.SetBool("TargetLostB", true);
    }
}
