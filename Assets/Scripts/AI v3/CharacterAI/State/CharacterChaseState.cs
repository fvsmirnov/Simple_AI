using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/States/Chase", fileName = "CharacterChaseState")]
public class CharacterChaseState : BaseChaseState
{
    public override void Init(BaseBrain brain)
    {
        brain.animator.SetBool("PatrolB", true);
    }
}
