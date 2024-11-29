using Settings;
using UnityEngine;
using UnityEngine.UI;

public class SetGamma : MonoBehaviour
{
    [SerializeField] private Slider gammaSlider;

    private void Start()
    {
        gammaSlider.value = Gamma.Instance.GammaValue;
    }

    public void SaveValue()
    {
        Gamma.Instance.SaveGammaValue();    
    }

    public void SetGammaValue()
    {
        Gamma.Instance.SetGammaValue(gammaSlider.value);
    }
}