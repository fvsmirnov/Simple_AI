using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    /// <summary>
    /// Execute once every time after state changed
    /// </summary>
    void Init(BaseBrain brain);

    /// <summary>
    /// Execute every tick
    /// </summary>
    void Execute(BaseBrain brain);

    /// <summary>
    /// Execute before current state will be disabled
    /// </summary>
    void Exit(BaseBrain brain);
}
