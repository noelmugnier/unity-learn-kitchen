using UnityEngine;

public interface IInteractable
{
    void Interact(ObjectHolderHandler player);
    void Select();
    void Unselect();
}

public interface ICanHoldKitchenObject
{
    Transform GetHoldingPoint();
}