using UnityEngine;

public class CuttingCounter : ClearCounter, IProcessable
{
    private new void Start()
    {
        base.Start();
    }

    public void Process(InteractionHandler player)
    {
        Debug.Log($"Process {ObjectOnTop}");
    }
}
