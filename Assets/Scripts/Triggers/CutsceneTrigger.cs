using UnityEngine;
using UnityEngine.Video;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _sounds;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _sounds.SetActive(false);
            Time.timeScale = 0;
            _videoPlayer.Play();
            _audioSource.Play();
        }
    }
}
