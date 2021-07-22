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


    public virtual BaseState Mutate(float? speed = null)
    {
        return new BaseState(
            speed ?? Speed
        );
    }
        
}
