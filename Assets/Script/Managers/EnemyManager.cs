using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyController> enemyList;

    private void Awake()
    {
        var preSpawnedEnemies = FindObjectsOfType<EnemyController>();
        foreach (var enemy in preSpawnedEnemies)
        {
            PrepareEnemy(enemy);
        }
    }

    /// <summary>
    /// Find closest enemy
    /// </summary>
    /// <param name="target">Point from which we are looking for closest enemy</param>
    /// <param name="checkCanSee">Optional parameter if we should check with raycast for other obstacles</param>
    /// <returns>Returns closest enemy or if we can't reach anyone or everyone are dead returns null</returns>
    public virtual EnemyController FindClosestEnemy(Vector3 target, bool checkCanSee = false)
    {
        float minDistance = float.PositiveInfinity;
        EnemyController closestEnemyController = null;
        foreach (var enemyController in enemyList)
        {
            Vector3 enemyTarget = enemyController.transform.position;
            float distance = Vector3.Distance(enemyTarget, target);
            bool canSee = true;
            if (checkCanSee)
            {
                canSee = Physics.Linecast(enemyTarget, target);
            }
            if (canSee && distance < minDistance)
            {
                closestEnemyController = enemyController;
                minDistance = distance;
            }
        }
        return closestEnemyController;
    }

    private void PrepareEnemy(EnemyController controller)
    {
        controller.OnDie += OnEnemyDie;
        enemyList.Add(controller);
    }

    private void OnEnemyDie(BaseCharacterController controller)
    {
        if (controller is EnemyController enemyController)
        {
            enemyList.Remove(enemyController);
        }
    }
}