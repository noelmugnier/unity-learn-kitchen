using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    
    private void Update()
    {
        HandleMovement();
    }

    private Vector3 GetDesiredDirection()
    {
        var inputDirection = gameInput.GetNormalizedMovementDirection();
        return new Vector3(inputDirection.x, 0f, inputDirection.y);
    }

    private void HandleMovement()
    {
        Debug.Log("handling");
        var desiredDirection = GetDesiredDirection();
        var currentPosition = transform.position;
        var maxDistance = moveSpeed * Time.deltaTime;
        
        if (TryMove(currentPosition, desiredDirection, maxDistance))
            return;

        var leftOrRightDirection = new Vector3(desiredDirection.x, 0f, 0f);
        if (TryMove(currentPosition, leftOrRightDirection, maxDistance))
            return;

        var upOrDownDirection = new Vector3(0f, 0f, desiredDirection.z);
        if (TryMove(currentPosition, upOrDownDirection, maxDistance))
            return;
        
        Debug.Log("Is not moving");
        IsMoving = false;
    }

    public bool IsMoving { get; private set; }

    private bool TryMove(Vector3 playerPosition, Vector3 moveDirection, float maxDistance)
    {
        var canMoveInDirection = CanMoveInDirection(playerPosition, moveDirection, maxDistance);
        if (!canMoveInDirection)
            return false;

        Move(moveDirection, maxDistance);
        return true;
    }

    private bool CanMoveInDirection(Vector3 currentPosition, Vector3 desiredDirection, float maxDistance)
    {
        return !Physics.CapsuleCast(currentPosition, currentPosition + Vector3.up, .7f, desiredDirection, maxDistance);
    }

    private void Move(Vector3 moveDirection, float maxDistance)
    {
        transform.position += moveDirection * maxDistance;
        transform.forward += Vector3.Slerp(transform.forward, moveDirection, 10f * Time.deltaTime);
        
        IsMoving = moveDirection != Vector3.zero;
    }
}
