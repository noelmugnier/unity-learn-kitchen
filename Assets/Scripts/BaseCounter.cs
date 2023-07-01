using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IInteractable
{
    private SelectedCounterVisual? _selectedCounterVisual;

    protected void Start()
    {
        _selectedCounterVisual = gameObject.GetComponentInChildren<SelectedCounterVisual>(includeInactive:true);
    }
    
    public abstract void Interact(ObjectHolderHandler player);

    public void Select()
    {
        if(_selectedCounterVisual != null)
            _selectedCounterVisual.ShowHighlight();
    }

    public void Unselect()
    {
        if(_selectedCounterVisual != null)
            _selectedCounterVisual.HideHighlight();
    }
}