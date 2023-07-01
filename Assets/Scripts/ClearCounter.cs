using System;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] protected Transform counterTopPoint;
    public KitchenObject? ObjectOnTop { get; private set; }
    public bool HasObjectOnTop => ObjectOnTop != null;
    
    public EventHandler<OnObjectPlacedArgs>? OnObjectPlaced;
    public EventHandler<OnObjectTakenArgs>? OnObjectTaken;

    private new void Start()
    {
        base.Start();
    }
    
    public override void Interact(ObjectHolderHandler holder)
    {
        switch (HasObjectOnTop)
        {
            case false when holder.IsHoldingObject:
                PlaceObject(holder.HoldedObject!);
                return;
            case true when !holder.IsHoldingObject:
                TakeObject();
                return;
        }
    }

    private void PlaceObject(KitchenObject kitchenObject)
    {
        if (HasObjectOnTop)
            return;
        
        ObjectOnTop = kitchenObject;
        ObjectOnTop.transform.SetParent(counterTopPoint);
        ObjectOnTop.transform.localPosition = Vector3.zero;
        
        OnObjectPlaced?.Invoke(this, new OnObjectPlacedArgs(kitchenObject));
    }

    private void TakeObject()
    {
        if (!HasObjectOnTop)
            return;

        ObjectOnTop!.transform.SetParent(null);
        ObjectOnTop.transform.localPosition = Vector3.zero;
        
        OnObjectTaken?.Invoke(this, new OnObjectTakenArgs(ObjectOnTop));
        ObjectOnTop = null;
    }
}

public class OnObjectTakenArgs : EventArgs
{
    public KitchenObject KitchenObject { get; }

    public OnObjectTakenArgs(KitchenObject kitchenObject)
    {
        KitchenObject = kitchenObject;
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