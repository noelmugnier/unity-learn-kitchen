using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public EventHandler<OnObjectHeldArgs>? OnObjectProduced;

    private new void Start()
    {
        base.Start();
        
        var spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>(includeInactive: true);
        spriteRenderer.sprite = kitchenObjectSO.Sprite;
    }

    public override void Interact(InteractionHandler player)
    {
        if (player.IsHoldingObject)
            return;

        var kitchenObject = Instantiate(kitchenObjectSO.Prefab).GetComponent<KitchenObject>();
        OnObjectProduced?.Invoke(this, new OnObjectHeldArgs(kitchenObject));
    }
}

public class OnObjectHeldArgs : EventArgs
{
    public KitchenObject KitchenObject { get; }

    public OnObjectHeldArgs(KitchenObject kitchenObject)
    {
        KitchenObject = kitchenObject;
    }
}