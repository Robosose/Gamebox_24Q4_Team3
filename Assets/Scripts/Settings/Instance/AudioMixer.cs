using Patterns.Singleton;

namespace Settings
{
    public class AudioMixer : Singleton<AudioMixer>
    {
        private float _music;
        private float _sound;

        public float Music => _music;

        public float Sound => _sound;

        public void SetMusicValue(float value)
        {
            _music = value;
            print("Music volume set");
        }

        public void SetSoundValue(float value)
        {
            _sound = value;
            print("Sound volume set");
        }
    }
}