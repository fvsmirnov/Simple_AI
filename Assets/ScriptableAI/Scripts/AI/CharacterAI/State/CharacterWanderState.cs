using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/States/Wander", fileName = "CharacterWanderState")]
public class CharacterWanderState : BaseWanderState
{
    public override void Init(BaseBrain brain)
    {
        brain.SetNavMeshSpeed(brain.normalSpeed);

        brain.ResetAnimatorState();
        brain.animator.SetBool("PatrolB", true);
    }
}
