using UnityEngine;

public interface IInteractable
{
    void Interact(InteractionHandler player);
    void Select();
    void Unselect();
}

public interface IProcessable
{
    void Process(InteractionHandler player);
}

public interface ICanHoldKitchenObject
{
    Transform GetHoldingPointTransform();
}