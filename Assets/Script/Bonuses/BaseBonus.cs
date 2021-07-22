public abstract class BaseBonus
{
    public delegate void BonusChanged(BaseBonus bonus);

    public event BonusChanged OnAddBonus;
    public event BonusChanged OnRemoveBonus;
    public event BonusChanged OnExpireBonus;

    public bool HasDuration { get; protected set; }
    public float Duration { get; protected set; }

    public virtual BaseState ApplyBonus(BaseState targetState) 
    {
        OnAddBonus?.Invoke(this);

        return targetState;
    }

    public virtual BaseState Update(BaseState targetState, float deltaTime)
    {
        if (!HasDuration) return targetState;

        Duration -= deltaTime;
        if (Duration <= 0)
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

    protected void SetDuration(float duration)
    {
        Duration = duration;
        HasDuration = true;
    }
}
