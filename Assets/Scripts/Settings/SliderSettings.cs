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
            _musicSlider.onValueChanged.AddListener(Sound.Instance.SetMusicValue);
            _soundSlider.onValueChanged.AddListener(Sound.Instance.SetSoundValue);         
        }

        private void OnDisable()
        {
            _musicSlider.onValueChanged.RemoveListener(Sound.Instance.SetMusicValue);
            _soundSlider.onValueChanged.RemoveListener(Sound.Instance.SetSoundValue);
        }
    }
}