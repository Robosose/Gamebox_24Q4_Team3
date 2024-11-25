using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

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
        _animator.SetBool("IsCrouched", isActive);
    }

    public void SetWalkCrouch(bool isActive)
    {
        _animator.SetBool("IsWalkCrouching", isActive);
    }

}
