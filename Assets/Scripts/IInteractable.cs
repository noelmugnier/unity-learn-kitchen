using UnityEngine;

public interface IInteractable
{
    void Select();
    void Unselect();
    bool Interact(Transform playerHoldingPoint);
    bool PutKitchenObject(KitchenObject kitchenObject);
}