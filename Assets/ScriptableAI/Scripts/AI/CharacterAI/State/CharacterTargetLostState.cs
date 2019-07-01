using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/States/TargetLost", fileName = "CharacterTargetLostState")]
public class CharacterTargetLostState : BaseTargetLostState
{
    IEnumerator searchTarget;
    IEnumerator targetLost;

    public override void Init(BaseBrain brain)
    {
        searchTarget = searchTarget.DoWhile(1.5f, null, () => brain.StartCoroutine(targetLost));
        targetLost = targetLost.DoWhile(3, () => TargetLostAnimation(brain), () => brain.ChangeState(typeof(BaseWanderState)));
        brain.StartCoroutine(searchTarget);
    }

    public override void Exit(BaseBrain brain)
    {
        brain.StopCoroutine(searchTarget);
        brain?.StopCoroutine(targetLost);
    }

    void TargetLostAnimation(BaseBrain brain)
    {
        brain.ResetAnimatorState();
        brain.animator.SetBool("TargetLostB", true);
    }
}
