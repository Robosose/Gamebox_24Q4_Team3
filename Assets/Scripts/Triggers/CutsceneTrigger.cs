using Settings;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private AudioSource _audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            _videoPlayer.Play();
            _audioSource.Play();
            Sound.Instance.SetMusicValue(-80f);
            Sound.Instance.SetSoundValue(-80f);
        }
    }
}
