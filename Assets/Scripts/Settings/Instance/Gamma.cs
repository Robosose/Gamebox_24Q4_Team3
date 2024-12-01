using Patterns.Singleton;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Gamma : Singleton<Gamma>
{
    [SerializeField] private VolumeProfile profile;
    private const string GammaSaveName = "GammaValue";
    private float _gammaValue = 0.5f;

    private void Start()
    {
        LoadGammaValue();
        SetGammaValue(_gammaValue);
    }

    private void LoadGammaValue()
    {
        if (ES3.KeyExists(GammaSaveName))
            _gammaValue = float.Parse(ES3.Load(GammaSaveName).ToString());
    }

    public float GetGammaValue()
    {
        if (ES3.KeyExists(GammaSaveName))
            return float.Parse(ES3.Load(GammaSaveName).ToString());
        return _gammaValue;
    }

    public void SetGammaValue(float value)
    {
        _gammaValue = value;
        profile.TryGet(out LiftGammaGain liftGammaGain);
        liftGammaGain.gamma.value = new Vector4(0, 0, 0, Mathf.Lerp(-0.5f, 0.5f, _gammaValue));
    }

    public void SaveGammaValue()
    {
        ES3.Save(GammaSaveName, _gammaValue);
    }

    private void OnApplicationQuit()
    {
        SaveGammaValue();
    }
}