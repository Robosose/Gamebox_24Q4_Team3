using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Transform door;
    private AudioSource _audioSource;
    private bool _isPlaying;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        _audioSource.Pause();
    }

    private void Update()
    {
        if(_isPlaying)
            return;
        
        if (Vector3.Distance(transform.position, door.position) < distance)
        {
            _audioSource.Play();
            _isPlaying = true;
        }
    }
}
