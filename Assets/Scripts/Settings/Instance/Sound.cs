using Patterns.Singleton;
using UnityEngine;
using UnityEngine.Audio;

namespace Settings
{
    public class Sound : Singleton<Sound>
    {
        [SerializeField] private AudioMixer soundMixer;
        [SerializeField] private AudioMixer musicMixer;
        private const string SoundSaveName = "SoundValue";
        private const string MusicSaveName = "MusicValue";
        
        private float _musicValue;
        private float _soundValue;

        public float MusicValue => _musicValue;
        public float SoundValue => _soundValue;

        private void Start()
        {
            LoadAudioValue();
            SetMusicValue(_musicValue);
            SetSoundValue(_soundValue);
        }

        private void LoadAudioValue()
        {
            if (ES3.KeyExists(SoundSaveName))
            {
                _soundValue = float.Parse(ES3.Load(SoundSaveName).ToString());
            }

            if(ES3.KeyExists(MusicSaveName))
                _musicValue = float.Parse(ES3.Load(MusicSaveName).ToString());
        }

        public void SetMusicValue(float value)
        {
            _musicValue = value;
            musicMixer.SetFloat("MasterVolume", Mathf.Lerp(-80f,10f,_musicValue));
        }

        public void SetSoundValue(float value)
        {
            _soundValue = value;
            soundMixer.SetFloat("MasterVolume",Mathf.Lerp(-80f,10f, _soundValue));
        }

        public void SaveAudioValue()
        {
            ES3.Save(MusicSaveName,_musicValue.ToString());
            ES3.Save(SoundSaveName,_soundValue.ToString());
        }

        private void OnApplicationQuit()
        {
            SaveAudioValue();
        }
    }
}