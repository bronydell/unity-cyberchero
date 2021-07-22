public class PlayerState : BaseState
{
    public PlayerState(float speed) : base(speed)
    {
    }

    public PlayerState(CharacterStarterInfo starerInfo) : base(starerInfo)
    {
    }

    public override BaseState Mutate(float? speed = null)
    {
        return new PlayerState(
            speed ?? Speed
        );
    }
}