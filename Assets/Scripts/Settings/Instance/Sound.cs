using System;
using Patterns.Singleton;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Sound : Singleton<Sound>
{
    [SerializeField] private AudioMixer soundMixer;
    [SerializeField] private AudioMixer musicMixer;
    
    private const string SoundSaveName = "SoundValue";
    private const string MusicSaveName = "MusicValue";

    private float _musicValue = 0.8f;
    private float _soundValue = 0.8f;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += RefreshAudio;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= RefreshAudio;
    }

    private void RefreshAudio(Scene scene, LoadSceneMode sceneMode)
    {
        LoadAudioValue();
        
        SetMusicValue(_musicValue);
        SetSoundValue(_soundValue);
    }

    private void LoadAudioValue()
    {
        if (ES3.KeyExists(SoundSaveName))
            _soundValue = float.Parse(ES3.Load(SoundSaveName).ToString());

        if (ES3.KeyExists(MusicSaveName))
            _musicValue = float.Parse(ES3.Load(MusicSaveName).ToString());
    }
    
    public float GetSoundValue()
    {
        if (ES3.KeyExists(SoundSaveName))
            return float.Parse(ES3.Load(SoundSaveName).ToString());

        return _soundValue;
    }
    
    public float GetMusicValue()
    {
        if (ES3.KeyExists(MusicSaveName))
            return float.Parse(ES3.Load(MusicSaveName).ToString());
        
        return _musicValue;
    }
    
    public void SetMusicValue(float value)
    {
        _musicValue = value;
        musicMixer.SetFloat("MasterVolume", Mathf.Lerp(-80f, 10f, _musicValue));
    }

    public void SetSoundValue(float value)
    {
        _soundValue = value;
        soundMixer.SetFloat("MasterVolume", Mathf.Lerp(-80f, 10f, _soundValue));
    }

    public void MuteMusicAndSound()
    {
        soundMixer.SetFloat("MasterVolume", -80f);
        musicMixer.SetFloat("MasterVolume", -80f);
    }

    public void SaveAudioValue()
    {
        ES3.Save(MusicSaveName, _musicValue.ToString());
        ES3.Save(SoundSaveName, _soundValue.ToString());
    }

    private void OnApplicationQuit()
    {
        SaveAudioValue();
    }
}