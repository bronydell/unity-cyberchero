using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBonus
{
    public delegate void OnBonusChanged(BaseBonus bonus);

    public event OnBonusChanged OnAddBonus;
    public event OnBonusChanged OnRemoveBonus;
    public event OnBonusChanged OnExpireBonus;

    [SerializeField]
    private bool hasDuration;
    [SerializeField]
    private float duration;

    public virtual BaseState ApplyBonus(BaseState targetState) 
    {
        OnAddBonus?.Invoke(this);

        return targetState;
    }

    public virtual BaseState Update(BaseState targetState, float deltaTime)
    {
        if (!hasDuration) return targetState;
        
        duration -= deltaTime;
        if (duration > 0)
        {
            OnExpireBonus?.Invoke(this);
        }


        return targetState;
    }

    public virtual BaseState RemoveBonus(BaseState targetState)
    {
        OnRemoveBonus?.Invoke(this);
        return targetState;
    }
}
