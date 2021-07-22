using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private PlayerController playerController;
    private Vector2 inputDirection;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        BaseState baseState = playerController.State;
        Vector3 movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        navMeshAgent.Move(movementDirection * baseState.Speed * Time.deltaTime);
    }

    private void OnMovement(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }
}
    
