public class HealthChangeBonus : BaseBonus
{
    protected float healthAdjustment;

    public HealthChangeBonus(float healthAdjustment, string source) : base(source)
    {
        this.healthAdjustment = healthAdjustment;
    }

    public override BaseState ApplyBonus(BaseState targetState)
    {
        return base.ApplyBonus(targetState).Mutate(
            health: targetState.Health + healthAdjustment
        );
    }

    public override BaseState RemoveBonus(BaseState targetState)
    {
        return base.ApplyBonus(targetState).Mutate(
            speed: targetState.Speed - healthAdjustment
        );
    }
}