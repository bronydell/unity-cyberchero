public class PlayerState : BaseState
{
    public PlayerState(float speed, float health, float maxHealth) 
        : base(speed, health, maxHealth)
    {
    }

    public PlayerState(CharacterStarterInfo starerInfo) : base(starerInfo)
    {
    }

    public override BaseState Mutate(
        float? speed = null,
        float? health = null,
        float? maxHealth = null)
    {
        return new PlayerState(
            speed ?? Speed,
            health ?? Health,
            maxHealth ?? MaxHealth
        );
    }
}