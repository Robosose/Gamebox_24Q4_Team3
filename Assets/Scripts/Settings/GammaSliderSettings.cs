using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class GammaSliderSettings : MonoBehaviour
    {
        [SerializeField] private Slider _gammaSlider;

        private void OnEnable()
        {
            _gammaSlider.onValueChanged.AddListener(Gamma.Instance.SetGammaValue);
        }

        private void OnDisable()
        {
            _gammaSlider.onValueChanged.RemoveListener(Gamma.Instance.SetGammaValue);
        }
    }
}