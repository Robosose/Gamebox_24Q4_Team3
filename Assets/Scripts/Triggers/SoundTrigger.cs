using System;
using UnityEngine;

namespace Scripts.Triggers
{
    public class TriggerSound : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private AudioSource _audioSource;

        private bool _hasSoundPlayed = false;

        private void Awake()
        {
            if (_audioSource == null || _audioClips == null || _audioClips.Length == 0)
                throw new ArgumentNullException(paramName: nameof(gameObject.name), message: "Name cannot be null");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_hasSoundPlayed)
            {
                PlaySound();
            }
        }

        public void PlaySound()
        {
            _hasSoundPlayed = true;

            foreach (var clip in _audioClips)
            {
                _audioSource.PlayOneShot(clip);
            }

            float maxClipLenght = GetMaxClipLength();
            Destroy(gameObject, maxClipLenght);
        }

        private float GetMaxClipLength()
        {
            float maxLength = 0f;

            foreach (var clip in _audioClips)
            {
                if (clip != null && clip.length > maxLength)
                {
                    maxLength = clip.length;
                }
            }

            return maxLength;
        }
    }
}