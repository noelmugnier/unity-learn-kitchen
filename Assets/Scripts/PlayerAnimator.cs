using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsWalking", player?.GetComponent<MoveHandler>()?.IsMoving ?? false);
    }
}
public class VisualContainerCounterAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsWalking", player?.GetComponent<MoveHandler>()?.IsMoving ?? false);
    }
}