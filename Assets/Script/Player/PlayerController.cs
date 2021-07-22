using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsSystem))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterStarterInfo starterInfo;
    
    private StatsSystem playerStats;

    public PlayerState State => (PlayerState)playerStats.State;

    private void Awake()
    {
        playerStats = GetComponent<StatsSystem>();
        playerStats.State = new PlayerState(starterInfo);
    }

    [ContextMenu("Give movement boost")]
    public void GiveMovementBonus()
    {
        playerStats.ApplyBonus(new MovementBonus(3, 10));
    }
    
}
