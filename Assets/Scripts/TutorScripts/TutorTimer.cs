using UnityEngine;

public class TutorTimer : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private Light _redLight;
    [SerializeField] private Light _greenLight;
    [SerializeField] private Light[] _light;

    [Header("Animation Settings")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string animationTriggerName = "PlayAnimation";

    [Header("Timer Settings")]
    [SerializeField] private float _triggerTime;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _lampSound;
    [SerializeField] private AudioClip _doorOpen;

    private float _timer;

    private bool _hasTriggered;

    private void Start()
    {
        if (_redLight != null) _redLight.enabled = true;
        if (_greenLight != null) _greenLight.enabled = false;

        _timer = 0f;
        _hasTriggered = false;
    }

    private void Update()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        _timer += Time.deltaTime;

        if (!_hasTriggered && _timer >= _triggerTime)
        {
            TriggerEvent();
            _hasTriggered = true;
        }
    }

    private void TriggerEvent()
    {
        if (_animator is null)
            return;
        
        _animator.SetTrigger(animationTriggerName);
        _audioSource.PlayOneShot(_doorOpen);
        _lampSound.Pause();

        if (_redLight != null) _redLight.enabled = false;
        if (_greenLight != null) _greenLight.enabled = true;

        foreach (Light light in _light)
        {
            light.enabled = false;
        }
    }
}

