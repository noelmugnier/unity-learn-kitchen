using System;
using System.Linq;
using UnityEngine;

public interface IInteractable
{
    void Select();
    void Unselect();
    void Interact();
}

public class ClearCounter : MonoBehaviour, IInteractable
{
    public void Select()
    {
        var selectedCounterVisual = gameObject.GetComponentInChildren<SelectedCounterVisual>(includeInactive:true);
        selectedCounterVisual.Enable();
    }

    public void Unselect()
    {
        var selectedCounterVisual = gameObject.GetComponentInChildren<SelectedCounterVisual>(includeInactive:true);
        selectedCounterVisual.Disable();
    }

    public void Interact()
    {
        Debug.Log("Interact");
    }
}