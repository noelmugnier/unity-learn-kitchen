
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}