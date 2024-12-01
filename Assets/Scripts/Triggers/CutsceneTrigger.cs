using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Video;
using Zenject;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private VideoClip enClip;
    [SerializeField] private VideoClip ruClip;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private AudioSource _audioSource;
    private InputManager _inputManager;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    private void OnEnable()
    {
        
        _videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnDisable()
    {
        _videoPlayer.loopPointReached -= OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        SceneSaver.Instance.LoadScene(ScenesType.MainMenuScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Sound.Instance.MuteMusicAndSound();
            _inputManager.ActionDisable();
            Time.timeScale = 0;
            _videoPlayer.clip = LanguageSwitcher.Instance.CurrentLocal == LanguageSwitcher.Instance.EnLocal ? enClip : ruClip;
            _videoPlayer.Play();
            _audioSource.Play();
        }
    }
}
