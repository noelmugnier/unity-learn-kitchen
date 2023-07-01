using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectHolderHandler : MonoBehaviour
{
    private List<BaseCounter> _countersInRange = new ();
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform playerHoldingPoint;
    
    public KitchenObject? HoldedObject { get; private set; }
    public bool IsHoldingObject => HoldedObject != null;
    
    private void Start()
    {
        gameInput.OnInteractAction += OnInteractAction;
    }

    private void OnInteractAction(object sender, EventArgs e)
    {
        var nearestCounter = GetNearestCounter();
        if (nearestCounter == null)
            return;
        
        if(IsInSight(nearestCounter.transform))
            nearestCounter.Interact(this);
    }

    private void OnTriggerStay(Collider other)
    {
        var counter = other.GetComponent<BaseCounter>();
        if (counter == null)
            return;
        
        var nearestCounter = GetNearestCounter();
        if (nearestCounter != counter || !IsInSight(counter.transform))
            counter.Unselect();
        else
            counter.Select();
    }

    private BaseCounter? GetNearestCounter()
    {
        return _countersInRange
            .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
            .FirstOrDefault();
    }

    private void OnTriggerEnter(Collider other)
    {
        var counter = other.GetComponent<BaseCounter>();
        if (counter == null)
            return;
        
        switch (counter)
        {
            case ClearCounter clearCounter:
                clearCounter.OnObjectPlaced += OnObjectPlaced;
                clearCounter.OnObjectTaken += OnObjectTaken;
                break;
            case ContainerCounter containerCounter:
                containerCounter.OnObjectProduced += OnObjectProduced;
                break;
        }
        
        _countersInRange.Add(counter);
    }

    private void OnTriggerExit(Collider other)
    {
        var counter = other.GetComponent<BaseCounter>();
        if (counter == null)
            return;
        
        counter.Unselect();
        _countersInRange.Remove(counter);
    }

    private bool IsInSight(Transform target)
    {
        var viewAngle = 70;
        var dir = target.position - transform.position;
        var angle = Vector3.Angle(transform.forward, dir);

        return angle <= viewAngle && Physics.Linecast(transform.position, target.position, out RaycastHit hitInfo) && hitInfo.transform == target;
    }

    private void OnObjectProduced(object sender, OnObjectProducedArgs e)
    {
        AssignObject(e.KitchenObject);
    }

    private void OnObjectTaken(object sender, OnObjectTakenArgs e)
    {
        AssignObject(e.KitchenObject);
    }

    private void AssignObject(KitchenObject kitchenObject)
    {
        if (IsHoldingObject)
            return;

        HoldedObject = kitchenObject;
        HoldedObject.transform.parent = playerHoldingPoint;
        HoldedObject.transform.localPosition = Vector3.zero;
    }

    private void OnObjectPlaced(object sender, OnObjectPlacedArgs e)
    {
        if (!IsHoldingObject)
            return;

        HoldedObject = null;
    }
}