using UnityEngine;

namespace Enemys
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioClip[] _clips;
        
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public void StartRunning()
        {
            _animator.SetBool(IsRunning, true);
        }
        public void StopRunning() => _animator.SetBool(IsRunning, false);
        public void StartWalking() => _animator.SetBool(IsWalking, true);
        public void StopWalking() => _animator.SetBool(IsWalking, false);
    }
}