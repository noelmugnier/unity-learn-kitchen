using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public EventHandler<OnObjectProducedArgs>? OnObjectProduced;

    private new void Start()
    {
        base.Start();
        
        var spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
        foreach (var spriteRenderer in spriteRenderers)
            spriteRenderer.sprite = kitchenObjectSO.Sprite;
    }

    public override void Interact(ObjectHolderHandler player)
    {
        if (player.IsHoldingObject)
            return;

        var kitchenObject = Instantiate(kitchenObjectSO.Prefab).GetComponent<KitchenObject>();
        OnObjectProduced?.Invoke(this, new OnObjectProducedArgs(kitchenObject));
    }
}

public class OnObjectProducedArgs : EventArgs
{
    public KitchenObject KitchenObject { get; }

    public OnObjectProducedArgs(KitchenObject kitchenObject)
    {
        KitchenObject = kitchenObject;
    }
}