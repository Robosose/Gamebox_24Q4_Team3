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
        [SerializeField] private AudioClip[] _monsterVoices;
        [Space]
        [SerializeField] private float _footstepIntervalWalk;
        [SerializeField] private float _footstepIntervalRun;
        [SerializeField] private float _monsterVoicesInterval;

        public float FootstepIntervalWalk => _footstepIntervalWalk;
        public float FootstepIntervalRun => _footstepIntervalRun;
        public float MonsterVoicesInterval => _monsterVoicesInterval;

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

            var randomClip = _footstepSounds[Random.Range(0, _footstepSounds.Length)];
            _audioSource.PlayOneShot(randomClip);
        }
        
        //Метод для воспроизведения голоса монстра PS Влад
        public void PlayMonsterVoices()
        {
            if (_monsterVoices.Length == 0 && !_audioSource) return;

            if (!_audioSource.enabled)
                _audioSource.enabled = true;

            var randomClip = _monsterVoices[Random.Range(0, _monsterVoices.Length)];
            _audioSource.PlayOneShot(randomClip);
        }
    }
}