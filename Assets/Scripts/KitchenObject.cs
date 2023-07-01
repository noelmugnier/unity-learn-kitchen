using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    public void AttachToParent(ICanHoldKitchenObject holder)
    {
        transform.SetParent(holder.GetHoldingPoint(), false);
        transform.localPosition = Vector3.zero;
    }
}