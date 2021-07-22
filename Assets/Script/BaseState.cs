using System;
using UnityEngine;

[Serializable]
public class BaseState
{
    public float Speed { get; }

    public BaseState(float speed)
    {
        Speed = speed;
    }

    public BaseState(CharacterStarterInfo starerInfo) 
        : this(starerInfo.Speed)
    {

    }

    public static BaseState operator +(BaseState stateA, BaseState stateB)
    {
        return new BaseState(stateA.Speed + stateB.Speed);
    }

    public static BaseState operator -(BaseState stateA, BaseState stateB)
    {
        return new BaseState(stateA.Speed - stateB.Speed);
    }
}
