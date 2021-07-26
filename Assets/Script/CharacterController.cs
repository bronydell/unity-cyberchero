using UnityEngine;

[RequireComponent(typeof(StatsSystem))]
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected CharacterStarterInfo starterInfo;

    protected StatsSystem stats;


    public delegate void DieEvent(CharacterController controller);

    public event DieEvent OnDie;

    protected virtual void Awake()
    {
        stats = GetComponent<StatsSystem>();
        stats.OnStateChanged += OnStateUpdate;
        stats.State = new PlayerState(starterInfo);
    }

    protected virtual void OnStateUpdate(BaseState oldState, BaseState newState)
    {
        if (newState.Health <= 0)
        {
            OnDie?.Invoke(this);
        }
    }

    protected virtual BaseState InitializeState()
    {
        return new BaseState(starterInfo);
    }
}