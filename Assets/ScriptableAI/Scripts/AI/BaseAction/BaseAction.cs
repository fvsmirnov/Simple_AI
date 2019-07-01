using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : ScriptableObject, IAction
{
    public abstract void DoAction(BaseBrain brain);
}
