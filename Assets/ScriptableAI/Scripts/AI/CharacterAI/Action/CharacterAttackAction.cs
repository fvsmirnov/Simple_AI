using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/Actions/Attack", fileName = "CharacterAttackAction")]
public class CharacterAttackAction : BaseAction
{
    public override void DoAction(BaseBrain brain)
    {
        brain.changeStatePossible = false;

        brain.ResetAnimatorState();
        brain.animator.SetBool("AttackB", true);
    }
}
