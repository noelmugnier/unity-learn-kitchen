using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask objectsLayerMask;
    
    private IInteractable _nearestInteractableObject;

    private const float PLAYER_RADIUS = .7f;
    private const float PLAYER_HEIGHT = 2f;
    private const float PLAYER_INTERACT_DISTANCE = 2f;

    private void Start()
    {
        gameInput.OnInteractAction += OnInteractAction;
    }

    void Update()
    {
        HandleMovement();
        HighlightNearestInteractableObjects();
    }

    private void HighlightNearestInteractableObjects()
    {
        var objectsInRange = Physics.OverlapSphere(transform.position, PLAYER_INTERACT_DISTANCE, objectsLayerMask);
        if (objectsInRange.Length > 1)
        {
            var newNearestInteractableObject = objectsInRange.Where(c => c.gameObject.GetComponent<IInteractable>() != null)
                .OrderBy(c => Vector3.Distance(transform.position, c.gameObject.transform.position))
                .Select(c => c.gameObject)
                .FirstOrDefault();
            
            _nearestInteractableObject?.Unselect();
            
            if (newNearestInteractableObject == null) 
                return;
            
            _nearestInteractableObject = newNearestInteractableObject.GetComponent<IInteractable>();
            _nearestInteractableObject.Select();
        }
        else
        {
            _nearestInteractableObject?.Unselect();
            _nearestInteractableObject = null;
        }
    }

    private void OnInteractAction(object sender, EventArgs e)
    {
        _nearestInteractableObject?.Interact();
    }

    private Vector3 GetPlayerDesiredDirection()
    {
        var playerInputDirection = gameInput.GetNormalizedMovementDirection();
        return new Vector3(playerInputDirection.x, 0f, playerInputDirection.y);
    }

    private void HandleMovement()
    {
        var playerDesiredDirection = GetPlayerDesiredDirection();
        var playerPosition = transform.position;
        var maxDistance = moveSpeed * Time.deltaTime;
        
        if (TryMove(playerPosition, playerDesiredDirection, maxDistance))
            return;

        var leftOrRightDirection = new Vector3(playerDesiredDirection.x, 0f, 0f);
        if (TryMove(playerPosition, leftOrRightDirection, maxDistance))
            return;

        var upOrDownDirection = new Vector3(0f, 0f, playerDesiredDirection.z);
        if (TryMove(playerPosition, upOrDownDirection, maxDistance))
            return;

        IsWalking = false;
    }

    private bool TryMove(Vector3 playerPosition, Vector3 moveDirection, float maxDistance)
    {
        var playerCanMoveToDirection = CanMoveInDirection(playerPosition, moveDirection, maxDistance);
        if (!playerCanMoveToDirection)
            return false;

        Move(moveDirection, maxDistance);
        return true;
    }

    private bool CanMoveInDirection(Vector3 playerPosition, Vector3 desiredDirection, float maxDistance)
    {
        var playerHeightVector = Vector3.up * PLAYER_HEIGHT;
        var playerTopPoint = playerPosition + playerHeightVector;

        return !Physics.CapsuleCast(playerPosition, playerTopPoint, PLAYER_RADIUS,
            desiredDirection, maxDistance);
    }

    private void Move(Vector3 moveDirection, float maxDistance)
    {
        transform.position += moveDirection * maxDistance;
        transform.forward += Vector3.Slerp(transform.forward, moveDirection, 10f * Time.deltaTime);
        
        IsWalking = moveDirection != Vector3.zero;
    }

    public bool IsWalking { get; private set; }
}