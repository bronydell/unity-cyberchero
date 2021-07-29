using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyManager enemyManager;
    [SerializeField]
    private PlayerController player;

    public PlayerController MainPlayer => player;
    public EnemyManager EnemyManager => enemyManager;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
}