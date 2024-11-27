using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SliderSettings : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;

        private void OnEnable()
        {
            _musicSlider.onValueChanged.AddListener(AudioMixer.Instance.SetMusicValue);
            _soundSlider.onValueChanged.AddListener(AudioMixer.Instance.SetSoundValue);         
        }

        private void OnDisable()
        {
            _musicSlider.onValueChanged.RemoveListener(AudioMixer.Instance.SetMusicValue);
            _soundSlider.onValueChanged.RemoveListener(AudioMixer.Instance.SetSoundValue);
        }
    }
}