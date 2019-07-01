using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Drone/Actions/Attack")]
public class DroneAttackAction : BaseAction
{
    public override void DoAction(BaseBrain brain)
    {
        Destroy(brain.Target);
    }
}
