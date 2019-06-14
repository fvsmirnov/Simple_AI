using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : ScriptableObject, IState
{
    public BaseAction[] actions;

    public virtual void Init(BaseBrain brain) { }

    public virtual void Execute(BaseBrain brain)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].DoAction(brain);
        }
    }

    public virtual void Exit(BaseBrain brain) { }

}