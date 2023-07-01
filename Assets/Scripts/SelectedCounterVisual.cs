using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    public void ShowHighlight()
    {
        gameObject.SetActive(true);
    }
    
    public void HideHighlight()
    {
        gameObject.SetActive(false);
    }
}