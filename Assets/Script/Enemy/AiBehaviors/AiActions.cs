using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : ActionBase
{
    private float remainingDistance;
    private float targetDistance;
    private Transform targetTransform;
    private Vector3 targetPoint;
    private NavMeshAgent agent;
    private bool canFindAWay;
    private float recalculatePathThreshold = 1;

    public FollowTarget(float targetDistance, Transform target, NavMeshAgent walkingAgent)
    {
        Name = "Follow target";
        targetTransform = target;
        this.targetDistance = targetDistance;
        remainingDistance = targetDistance;
        agent = walkingAgent;
    }

    protected override void OnStart()
    {
        base.OnStart();
        RecalculatePath();
    }

    protected override TaskStatus OnUpdate()
    {

        if ((targetPoint - targetTransform.position).sqrMagnitude > recalculatePathThreshold)
        {
            RecalculatePath();
        }
        if (agent.pathPending)
        {
            return TaskStatus.Continue;
        }

        if (IsTargetWithinReach())
        {
            agent.isStopped = true;
            return TaskStatus.Success;
        }

        if (!canFindAWay)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Continue;
    }

    protected override void OnExit()
    {
        base.OnExit();
        agent.isStopped = true;
    }

    private bool IsTargetWithinReach()
    {
        return Vector3.Distance(agent.transform.position, targetTransform.position) < targetDistance;
    }

    private void RecalculatePath()
    {
        if (IsTargetWithinReach()) 
        {
            agent.isStopped = true;
        }
        canFindAWay = agent.SetDestination(targetTransform.position);
        targetPoint = targetTransform.position;
        agent.isStopped = false;
    }
}

public static class AiActionsExtensions
{
    public static BehaviorTreeBuilder FollowTarget(this BehaviorTreeBuilder builder, Transform target, NavMeshAgent agent, float distance)
    {
        return builder.AddNode(new FollowTarget(distance, target, agent));
    }
}
