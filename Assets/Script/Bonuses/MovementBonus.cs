
public class MovementBonus : BaseBonus
{
    protected float speedModifier;

    public MovementBonus(float speedModifier, string source = "Unknown") : base(source)
    {
        this.speedModifier = speedModifier;
    }
    public MovementBonus(float duration, float speedModifier, string source = "Unknown") : this(speedModifier, source)
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
