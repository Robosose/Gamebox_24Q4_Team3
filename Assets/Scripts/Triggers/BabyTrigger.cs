using UnityEngine;
using UnityEngine.Audio;

public class BabyTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(1, 30)] private float _stopSound;
    [SerializeField] private AudioSource _audioSource;

    private float _timer;
    private bool _isPlayerInside;

    private void Start()
    {
        if (_audioSource == null)
            return;
        _audioSource.Stop();
    }

    private void Update()
    {
        if(_isPlayerInside)
        {
            _timer += Time.deltaTime;

            if(_timer > _stopSound)
            {
                StopSound();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isPlayerInside = true;
            GetComponent<Collider>().enabled = false;
            StartSound();
        }
    }

    private void StartSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void StopSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
