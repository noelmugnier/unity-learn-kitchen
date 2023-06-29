using JetBrains.Annotations;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IInteractable
{
    [SerializeField] protected Transform counterTopPoint;
    protected KitchenObject _currentKitchenObject;
    
    private SelectedCounterVisual _selectedCounterVisual;

    private void Start()
    {
        _selectedCounterVisual = gameObject.GetComponentInChildren<SelectedCounterVisual>(includeInactive:true);
    }

    public void Select() 
    {
        _selectedCounterVisual.Enable();
    }

    public void Unselect()
    {
        _selectedCounterVisual.Disable();
    }

    public virtual bool PutKitchenObject(KitchenObject kitchenObject)
    {
        if (_currentKitchenObject)
            return false;
        
        _currentKitchenObject = kitchenObject;
        _currentKitchenObject.transform.parent = counterTopPoint;
        _currentKitchenObject.transform.localPosition = Vector3.zero;
        
        return true;
    }

    [CanBeNull]
    public virtual bool Interact(Transform playerHoldingPoint)
    {
        if (_currentKitchenObject == null)
            return false;

        _currentKitchenObject.transform.parent = playerHoldingPoint;
        _currentKitchenObject.transform.localPosition = Vector3.zero;
        _currentKitchenObject = null;
        
        return true;
    }
}