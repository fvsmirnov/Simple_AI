using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/Actions/PrepareAttack", fileName = "CharacterPrepareAttackAction")]
public class CharacterPrepareAttackAction : BaseAction
{
    public override void DoAction(BaseBrain brain)
    {
        brain.changeStatePossible = true;

        //brain.animator.SetTrigger("PrepareAttack");
        brain.animator.SetBool("PatrolB", false);
        brain.animator.SetBool("AttackB", false);
        brain.animator.SetBool("TargetLostB", false);
        brain.animator.SetBool("PrepareAttackB", true);
    }
}
