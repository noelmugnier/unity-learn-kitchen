using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override bool Interact(Transform playerHoldPoint)
    {
        if (_currentKitchenObject != null)
        {
            _currentKitchenObject.transform.parent = playerHoldPoint;
            _currentKitchenObject.transform.localPosition = Vector3.zero;
            _currentKitchenObject = null;
            return true;
        }
        
        var spawnedGameObject = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
        spawnedGameObject.localPosition = Vector3.zero;
    
        _currentKitchenObject = spawnedGameObject.GetComponent<KitchenObject>();
        return true;
    }
}