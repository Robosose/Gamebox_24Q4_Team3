using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalk(bool isActive)
    {
        _animator.SetBool("IsWalking", isActive);
    }

    public void SetSprint(bool isActive)
    {
        _animator.SetBool("IsSprinting", isActive);
    }

    public void SetCrouch(bool isActive)
    {
        _animator.SetBool("IsCrouching", isActive);
    }

}
