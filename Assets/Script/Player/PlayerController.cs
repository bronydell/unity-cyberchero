using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerUI))]
public class PlayerController : BaseCharacterController
{
    [SerializeField]
    private GameObject weaponPrefab;
    [SerializeField]
    private Transform weaponSocket;
    [SerializeField]
    private float enemySearchCooldown;
    
    private BaseWeapon weapon;
    private PlayerUI playerUI;

    private EnemyController targetEnemy;

    public PlayerState State => (PlayerState)stats.State;

    protected override void Awake()
    {
        base.Awake();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError($"GameManager cannot be found on scene, make sure you've created it");
        }
        var weaponObj = Instantiate(weaponPrefab, weaponSocket);
        weapon = weaponObj.GetComponent<BaseWeapon>();
        playerUI = GetComponent<PlayerUI>();
        stats.OnStateChanged += playerUI.OnStateUpdate;
    }

    protected override void Start()
    {
        base.Start();
        // Trigger the weapon, because we can't be sure if this callback will be called at start
        StopMovementCallback();
        StartCoroutine(CheckForTargets());
    }

    private void Update()
    {
        // TODO: Make sure we do not rotate on anything besides X axis
        if (targetEnemy != null)
        {
            Transform selfTransform = weapon.transform;
            Transform targetTransform = targetEnemy.transform;
            Quaternion oldRotation = transform.rotation;
            var qTo = Quaternion.LookRotation(targetTransform.position - selfTransform.position);
            Debug.Log($"Old rotation {oldRotation.eulerAngles} and new {qTo.eulerAngles}?");
            transform.rotation = qTo;
        }
        else
        {
            Debug.Log("Can't find the enemy?");
        }
    }

    public void StartMovementCallback()
    {
        weapon.StopUse();

    }

    public void StopMovementCallback()
    {
        weapon.StartUse();
        FindClosestEnemy();
    }

    private IEnumerator CheckForTargets()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySearchCooldown);
            FindClosestEnemy();
        }
    }

    private void FindClosestEnemy()
    {
        targetEnemy = gameManager.EnemyManager.FindClosestEnemy(weapon.transform.position);
    }

    protected override BaseState InitializeState()
    {
        return new PlayerState(starterInfo);
    }
}
