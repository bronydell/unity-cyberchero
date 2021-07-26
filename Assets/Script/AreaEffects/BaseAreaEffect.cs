using System.Threading;
using UnityEngine;

public abstract class BaseAreaEffect : MonoBehaviour
{
    public delegate void OnAreaCollideWithoutStatSystem(GameObject enteredObject);
    public delegate bool OnAreaCollideWithStatSystem(StatsSystem enteredStatsSystem);
    public delegate void OnAreaEffectedActivated(StatsSystem activatedStatsSystem);

    public event OnAreaCollideWithoutStatSystem OnObjectAreaCollide;
    public event OnAreaCollideWithStatSystem OnObjectAreaEnter;
    public event OnAreaEffectedActivated OnObjectAreaActivate;

    public void OnTriggerEnter(Collider collider)
    {
        GameObject enteredGameObject = collider.gameObject;
        var statsSystem = enteredGameObject.GetComponent<StatsSystem>();
        if (statsSystem == null)
        {
            OnObjectAreaCollide?.Invoke(enteredGameObject);
            return;
        }
        bool? shouldExecuteEffect = OnObjectAreaEnter?.Invoke(statsSystem);
        if (!shouldExecuteEffect.HasValue || shouldExecuteEffect.Value)
        {
            ExecuteEffect(statsSystem);
            OnObjectAreaActivate?.Invoke(statsSystem);
        }
    }

    private void ExecuteEffect(StatsSystem targetStatSystem)
    {
        targetStatSystem.ApplyBonus(GetBonus());
    }

    protected abstract BaseBonus GetBonus();

}