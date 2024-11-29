using Settings;
using UnityEngine;
using UnityEngine.UI;

public class SetAudio : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private void Start()
    {
        musicSlider.value = Sound.Instance.MusicValue;
        soundSlider.value = Sound.Instance.SoundValue;
    }

    public void SaveValue()
    {
        Sound.Instance.SaveAudioValue();    
    }
    
    public void SetMusicValue()
    {
        Sound.Instance.SetMusicValue(musicSlider.value);
    }
    
    public void SetSoundValue()
    {
        Sound.Instance.SetSoundValue(soundSlider.value);
    }
}
