using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterUI))]
public class EnemyController : BaseCharacterController
{
    private CharacterUI characterUi;
    [SerializeField]
    private NavMeshAgent agent;

    public BaseState State => stats.State;

    [SerializeField]
    private BehaviorTree behaviorTree;

    private bool isDead = false;

    protected override void Awake()
    {
        base.Awake();
        characterUi = GetComponent<CharacterUI>();
        stats.OnStateChanged += characterUi.OnStateUpdate;
        OnDie += controller => isDead = true;
    }

    protected override void Start()
    {
        base.Start();
        behaviorTree = new BehaviorTreeBuilder(gameObject)
            .Sequence("If player is still alive, move to his position")
                .Condition("If not dead", () => !isDead)
                    .FollowTarget(gameManager.MainPlayer.transform, agent, 2)
            .End()
        .Build();
    }

    protected override void OnStateUpdate(BaseState oldState, BaseState newState)
    {
        base.OnStateUpdate(oldState, newState);
        if (oldState == null || oldState.Speed != newState.Speed)
        {
            agent.speed = newState.Speed;
        }
    }

    protected void Update()
    {
        if (!isDead)
        {
            behaviorTree.Tick();
        }
    }
}