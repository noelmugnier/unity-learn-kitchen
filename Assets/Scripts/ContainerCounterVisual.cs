using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnObjectProduced += OnObjectProduced; 
    }

    private void OnObjectProduced(object sender, OnObjectHeldArgs e)
    {
        _animator.SetTrigger("OpenClose");
    }
}
