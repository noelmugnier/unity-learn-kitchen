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
        Debug.Log(player.IsWalking);
        _animator.SetBool("IsWalking", player.IsWalking);
    }
}