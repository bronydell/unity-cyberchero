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
    private float movementSpeed;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        BaseState baseState = playerController.State;
        Vector3 movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        Vector3 scaledMovementDirection = movementDirection * baseState.Speed;

        float currentMovementSpeed = scaledMovementDirection.magnitude;

        if (movementSpeed == 0 && currentMovementSpeed != 0)
        {
            playerController.StartMovementCallback();
        }
        else if (movementSpeed != 0 && currentMovementSpeed == 0)
        {
            playerController.StopMovementCallback();
        }
        movementSpeed = currentMovementSpeed;
        navMeshAgent.Move(scaledMovementDirection * Time.deltaTime);
    }

    private void OnMovement(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }
}
    
