using System;
using UnityEngine;

public class ClearCounter : BaseCounter, ICanHoldKitchenObject
{
    [SerializeField] protected Transform counterTopPoint;
    public KitchenObject? ObjectOnTop { get; private set; }
    public bool HasObjectOnTop => ObjectOnTop != null;
    
    public EventHandler<OnObjectPlacedArgs>? OnObjectPlaced;
    public EventHandler<OnObjectHeldArgs>? OnObjectTaken;

    private new void Start()
    {
        base.Start();
    }
    
    public override void Interact(ObjectHolderHandler holder)
    {
        switch (HasObjectOnTop)
        {
            case false when holder.IsHoldingObject:
                PlaceObject(holder.HeldObject!);
                return;
            case true when !holder.IsHoldingObject:
                TakeObject();
                return;
        }
    }

    private void PlaceObject(KitchenObject kitchenObject)
    {
        ObjectOnTop = kitchenObject;
        ObjectOnTop.AttachToParent(this);
        
        OnObjectPlaced?.Invoke(this, new OnObjectPlacedArgs(ObjectOnTop));
    }

    private void TakeObject()
    {
        OnObjectTaken?.Invoke(this, new OnObjectHeldArgs(ObjectOnTop));
        ObjectOnTop = null;
    }

    public Transform GetHoldingPoint()
    {
        return counterTopPoint.transform;
    }
}

public class OnObjectPlacedArgs : EventArgs
{
    public KitchenObject KitchenObject { get; }

    public OnObjectPlacedArgs(KitchenObject kitchenObject)
    {
        KitchenObject = kitchenObject;
    }
}