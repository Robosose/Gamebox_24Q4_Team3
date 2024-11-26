using UnityEngine;

namespace Enemys
{
    public class EnemyView : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private Animator _animator;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        //Поля Влада
        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _footstepSounds;

        public void StartRunning() => _animator.SetBool(IsRunning, true);
        public void StopRunning() => _animator.SetBool(IsRunning, false);
        public void StartWalking() => _animator.SetBool(IsWalking, true);
        public void StopWalking() => _animator.SetBool(IsWalking, false);


        //Тестовая версия метода для звука шагов врага PS Влад
        public void PlayRandomFootstep()
        {
            if (_footstepSounds.Length == 0 && !_audioSource) return;

            if(!_audioSource.enabled)
                _audioSource.enabled = true;

            if (!_audioSource.gameObject.activeInHierarchy)
            {
                Debug.LogWarning("AudioSource is on an inactive obj");
                return;
            }

            var randomClip = _footstepSounds[Random.Range(0, _footstepSounds.Length)];
            _audioSource.PlayOneShot(randomClip);
        }
    }
}