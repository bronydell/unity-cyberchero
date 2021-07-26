using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyManager enemyManager;

    public EnemyManager EnemyManager => enemyManager;
}