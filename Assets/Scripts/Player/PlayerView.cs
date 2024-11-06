using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    public void UpdateWalkingAnimation()
    {
        _animator.SetBool(IsWalking, true);
    }
}
