using Bell;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class BellSoundManager : MonoBehaviour
{
    [SerializeField, Range(0.1f, 3f)] private float _maxVolum;
    [SerializeField, Range(0.1f, 3f)] private float _sensitivity;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBellSound(float velocity)
    {
        _audioSource.volume = Mathf.Clamp(velocity * _sensitivity, 0, _maxVolum);

        if(!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        
    }
}
