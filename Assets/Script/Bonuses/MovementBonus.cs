
public class MovementBonus : BaseBonus
{
    protected float speedModifier;

    public MovementBonus(float speedModifier)
    {
        this.speedModifier = speedModifier;
    }
    public MovementBonus(float duration, float speedModifier) : this(speedModifier)
    {
        this.SetDuration(duration);
    }

    public override BaseState ApplyBonus(BaseState targetState)
    {
        return base.ApplyBonus(targetState).Mutate(
            speed: targetState.Speed + speedModifier
        );
    }

    public override BaseState RemoveBonus(BaseState targetState)
    {
        return base.ApplyBonus(targetState).Mutate(
            speed: targetState.Speed - speedModifier
        );
    }
}
