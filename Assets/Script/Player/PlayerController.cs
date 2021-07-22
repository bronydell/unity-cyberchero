using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterStarterInfo starterInfo;
    public PlayerState State { get; set; }

    private void Awake()
    {
        State = new PlayerState(starterInfo);
    }
    
}
