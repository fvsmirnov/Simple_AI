using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Character/Actions/Attack", fileName = "CharacterAttackAction")]
public class CharacterAttackAction : BaseAction
{
    //public Collider damagableCollider;

    public override void DoAction(BaseBrain brain)
    {
        brain.changeStatePossible = false;

        brain.animator.SetBool("PatrolB", false);
        brain.animator.SetBool("PrepareAttackB", false);
        brain.animator.SetBool("TargetLostB", false);
        brain.animator.SetBool("AttackB", true);
    }
}
