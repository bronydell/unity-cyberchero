using UnityEngine;

[RequireComponent(typeof(CharacterUI))]
public class EnemyController : CharacterController
{
    private CharacterUI characterUi;

    public BaseState State => stats.State;


    protected override void Awake()
    {
        base.Awake();
        characterUi = GetComponent<CharacterUI>();
        stats.OnStateChanged += characterUi.OnStateUpdate;
    }
}