using UnityEngine;

[RequireComponent(typeof(StatsSystem))]
public class BaseCharacterController : MonoBehaviour
{
    [SerializeField]
    protected CharacterStarterInfo starterInfo;

    protected StatsSystem stats;

    protected GameManager gameManager;


    public delegate void DieEvent(BaseCharacterController controller);

    public event DieEvent OnDie;

    protected virtual void Awake()
    {
        stats = GetComponent<StatsSystem>();
        stats.OnStateChanged += OnStateUpdate;
        gameManager = FindObjectOfType<GameManager>();
    }

    protected virtual void Start()
    {
        stats.State = InitializeState();
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