using UnityEngine;
using UnityEngine.UI;

public class SetGamma : MonoBehaviour
{
    [SerializeField] private Slider gammaSlider;

    private void Start()
    {
        gammaSlider.value = Gamma.Instance.GetGammaValue();
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